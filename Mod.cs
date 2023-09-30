using p4g64.colourfulPartyPanel.Configuration;
using p4g64.colourfulPartyPanel.NuGet.templates.defaultPlus;
using p4g64.colourfulPartyPanel.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X64;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory;
using Reloaded.Mod.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static p4g64.colourfulPartyPanel.Colour;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace p4g64.colourfulPartyPanel;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public unsafe class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private IHook<RenderPartyPanelDelegate> _renderPartyPanelHook;
    private IHook<BtlRenderPartyPanelDelegate> _btlRenderPartyPanelHook;
    private IAsmHook _btlPartyPanelHook;

    //private Dictionary<PartyMember, ColourStruct> _fgColours = new();
    //private Dictionary<PartyMember, ColourStruct> _bgColours = new();
    private ColourStruct* _fgColours;
    private ColourStruct* _bgColours;

    private Memory _memory;

    public Mod(ModContext context)
    {
        //Debugger.Launch();
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;
        _memory = Memory.Instance;

        Utils.Initialise(_logger, _configuration, _modLoader);

        _bgColours = (ColourStruct*)_memory.Allocate((nuint)(sizeof(ColourStruct) * 9)).Address;
        _fgColours = (ColourStruct*)_memory.Allocate((nuint)(sizeof(ColourStruct) * 9)).Address;
        SetupColours();

        Utils.SigScan("40 53 56 57 41 57 48 83 EC 68", "RenderPartyPanel", address =>
        {
            _renderPartyPanelHook = _hooks.CreateHook<RenderPartyPanelDelegate>(RenderPartyPanel, address).Activate();
        });

        Utils.SigScan("40 55 53 57 41 55 41 56 41 57", "BtlRenderPartyPanel", address =>
        {
            _btlRenderPartyPanelHook = _hooks.CreateHook<BtlRenderPartyPanelDelegate>(BtlRenderPartyPanel, address).Activate();
        });

        Utils.SigScan("8B 05 ?? ?? ?? ?? 8B 0D ?? ?? ?? ?? 89 44 24 ?? 8B 05 ?? ?? ?? ?? 89 4C 24 ?? E9 ?? ?? ?? ??", "BtlColourPtr", address =>
        {
            string[] function = new string[]
            {
                "use64",
                "push rbx",
                "xor rbx, rbx", // clear rbx
                $"mov bx, [rdx + 2]", // get party member id
                "shl rbx, 2", // Multiply id by 4 (length of a colour)
                $"mov ecx, [qword {(nuint)_bgColours} + rbx]", // set bg colour
                $"mov eax, [qword {(nuint)_fgColours} + rbx]", // set fg colour
                "pop rbx"
            };
            _btlPartyPanelHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
        });
    }

    private void SetupColours()
    {
        foreach (PartyMember member in (PartyMember[])Enum.GetValues(typeof(PartyMember)))
        {
            if (member != PartyMember.Rise && member != PartyMember.None)
            {
                var fg = (Colour)_configuration.GetType().GetProperty($"{member}FgColour").GetValue(_configuration);
                _fgColours[(int)member] = new ColourStruct { R = fg.R, G = fg.G, B = fg.B, A = fg.A };
                var bg = (Colour)_configuration.GetType().GetProperty($"{member}BgColour").GetValue(_configuration);
                _bgColours[(int)member] = new ColourStruct { R = bg.R, G = bg.G, B = bg.B, A = bg.A };
            }
        }
    }

    private void BtlRenderPartyPanel(nuint param_1, BtlPartyPanelInfo* info, float param_3, float param_4, ushort param_5)
    {
        var member = info->MemberInfo->Member;
        _btlRenderPartyPanelHook.OriginalFunction(param_1, info, param_3, param_4, param_5);
    }

    private void RenderPartyPanel(nuint param_1, PartyPanelInfo* info)
    {
        for (int i = 0; i < info->NumPartyMembers; i++)
        {
            var member = &info->MemberInfo + i;
            SetColour(&member->Sprites->ShadowSprite, _bgColours[(int)member->PartyMember]);
            SetColour(&member->Sprites->OutlineSprite, _fgColours[(int)member->PartyMember]);
        }
        _renderPartyPanelHook.OriginalFunction(param_1, info);
    }

    private void SetColour(PartyPanelSprite* sprite, ColourStruct colour)
    {
        for (int i = 0; i < 8; i++)
        {
            (&sprite->RenderPrimitives + i)->Colour = colour;
        }
    }

    [Function(CallingConventions.Microsoft)]
    private delegate void RenderPartyPanelDelegate(nuint param_1, PartyPanelInfo* info);

    [Function(CallingConventions.Microsoft)]
    private delegate void BtlRenderPartyPanelDelegate(nuint param_1, BtlPartyPanelInfo* info, float param_3, float param_4, ushort param_5);

    [StructLayout(LayoutKind.Sequential)]
    public struct ColourStruct
    {
        internal byte R, G, B, A;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct BtlPartyPanelInfo
    {
        [FieldOffset(0xcf0)]
        internal PartyMemberInfo* MemberInfo;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct PartyMemberInfo
    {
        [FieldOffset(2)]
        internal PartyMember Member;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct PartyPanelInfo
    {
        // Actually an array of 4
        [FieldOffset(0)]
        internal PartyPanelSpriteInfo MemberInfo;

        [FieldOffset(0x3638)]
        internal int NumPartyMembers;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct PartyPanelSpriteInfo
    {
        [FieldOffset(0)]
        internal PartyPanelSprites* Sprites;

        [FieldOffset(0x32)]
        internal short Hp;

        [FieldOffset(0x34)]
        internal short MaxHp;

        [FieldOffset(0x36)]
        internal short Sp;

        [FieldOffset(0x38)]
        internal short MaxSp;

        [FieldOffset(0x3A)]
        internal PartyMember PartyMember;

        [FieldOffset(0x47)]
        byte unk; // just to make it the right length
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct PartyPanelSprites
    {
        [FieldOffset(0x278)]
        internal PartyPanelSprite ShadowSprite;

        [FieldOffset(0x528)]
        internal PartyPanelSprite OutlineSprite;

        [FieldOffset(0x7D8)]
        internal PartyPanelSprite CharacterSprite;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct PartyPanelSprite
    {
        // This is an array of 8
        [FieldOffset(0xC)]
        internal RenderPrimitive RenderPrimitives;

        // This is an array of 8
        [FieldOffset(0x1EC)]
        internal RenderPrimitive RenderPrimitives2;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct RenderPrimitive
    {
        [FieldOffset(8)]
        internal ColourStruct Colour;

        [FieldOffset(0x14)]
        int unk; // Just to make it the right length
    }

    private enum PartyMember : short
    {
        None,
        Protagonist,
        Yosuke,
        Chie,
        Yukiko,
        Rise,
        Kanji,
        Naoto,
        Teddie,
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
        SetupColours();
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}