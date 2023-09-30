using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace p4g64.colourfulPartyPanel;

public class Colour
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public Colour(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    //Protag
    public static readonly Colour ProtagonistFgColour = new Colour(131, 134, 139, 255);
    public static readonly Colour ProtagonistBgColour = new Colour(85, 87, 91, 255);

    // Yosuke
    public static readonly Colour YosukeFgColour = new Colour(210, 142, 87, 255);
    public static readonly Colour YosukeBgColour = new Colour(142, 90, 49, 255);

    // Chie
    public static readonly Colour ChieFgColour = new Colour(108, 169, 77, 255);
    public static readonly Colour ChieBgColour = new Colour(67, 112, 44, 255);

    // Yukiko
    public static readonly Colour YukikoFgColour = new Colour(210, 56, 49, 255);
    public static readonly Colour YukikoBgColour = new Colour(147, 32, 27, 255);

    // Kanji
    public static readonly Colour KanjiFgColour = new Colour(208, 185, 127, 255);
    public static readonly Colour KanjiBgColour = new Colour(137, 120, 77, 255);

    // Naoto
    public static readonly Colour NaotoFgColour = new Colour(70, 70, 131, 255);
    public static readonly Colour NaotoBgColour = new Colour(46, 46, 91, 255);

    // Teddie
    public static readonly Colour TeddieFgColour = new Colour(247, 170, 148, 255);
    public static readonly Colour TeddieBgColour = new Colour(166, 109, 92, 255);
}


