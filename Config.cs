using p4g64.colourfulPartyPanel.Template.Configuration;
using System.ComponentModel;
using static p4g64.colourfulPartyPanel.Colour;

namespace p4g64.colourfulPartyPanel.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Cycle Colours")]
    [Description("Makes the colours cycle, very cool gamer moment.")]
    [DefaultValue(false)]
    public bool CycleColours { get; set; } = false;

    [DisplayName("Protagonist Foreground Colour")]
    [Description("The colour of the foreground of the Protagonist's party panel")]
    public Colour ProtagonistFgColour { get; set; } = Colour.ProtagonistFgColour;

    [DisplayName("Protagonist Background Colour")]
    [Description("The colour of the background of the Protagonist's party panel")]
    public Colour ProtagonistBgColour { get; set; } = Colour.ProtagonistBgColour;

    // Yosuke
    [DisplayName("Yosuke Foreground Colour")]
    [Description("The colour of the foreground of Yosuke's party panel")]
    public Colour YosukeFgColour { get; set; } = Colour.YosukeFgColour;

    [DisplayName("Yosuke Background Colour")]
    [Description("The colour of the background of Yosuke's party panel")]
    public Colour YosukeBgColour { get; set; } = Colour.YosukeBgColour;

    // Chie
    [DisplayName("Chie Foreground Colour")]
    [Description("The colour of the foreground of Chie's party panel")]
    public Colour ChieFgColour { get; set; } = Colour.ChieFgColour;

    [DisplayName("Chie Background Colour")]
    [Description("The colour of the background of Chie's party panel")]
    public Colour ChieBgColour { get; set; } = Colour.ChieBgColour;

    // Yukiko
    [DisplayName("Yukiko Foreground Colour")]
    [Description("The colour of the foreground of Yukiko's party panel")]
    public Colour YukikoFgColour { get; set; } = Colour.YukikoFgColour;

    [DisplayName("Yukiko Background Colour")]
    [Description("The colour of the background of Yukiko's party panel")]
    public Colour YukikoBgColour { get; set; } = Colour.YukikoBgColour;

    // Kanji
    [DisplayName("Kanji Foreground Colour")]
    [Description("The colour of the foreground of Kanji's party panel")]
    public Colour KanjiFgColour { get; set; } = Colour.KanjiFgColour;

    [DisplayName("Kanji Background Colour")]
    [Description("The colour of the background of Kanji's party panel")]
    public Colour KanjiBgColour { get; set; } = Colour.KanjiBgColour;

    // Naoto
    [DisplayName("Naoto Foreground Colour")]
    [Description("The colour of the foreground of Naoto's party panel")]
    public Colour NaotoFgColour { get; set; } = Colour.NaotoFgColour;

    [DisplayName("Naoto Background Colour")]
    [Description("The colour of the background of Naoto's party panel")]
    public Colour NaotoBgColour { get; set; } = Colour.NaotoBgColour;

    // Teddie
    [DisplayName("Teddie Foreground Colour")]
    [Description("The colour of the foreground of Teddie's party panel")]
    public Colour TeddieFgColour { get; set; } = Colour.TeddieFgColour;

    [DisplayName("Teddie Background Colour")]
    [Description("The colour of the background of Teddie's party panel")]
    public Colour TeddieBgColour { get; set; } = Colour.TeddieBgColour;


    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}