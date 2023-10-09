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


    // From KC Tucker on stack exchange :)
    // https://arduino.stackexchange.com/a/35769
    public static readonly byte[] ColourCycle = new byte[] {
      0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 15, 17, 18, 20, 22, 24, 26, 28, 30, 32, 35, 37, 39,
     42, 44, 47, 49, 52, 55, 58, 60, 63, 66, 69, 72, 75, 78, 81, 85, 88, 91, 94, 97, 101, 104, 107, 111, 114, 117, 121, 124, 127, 131, 134, 137,
    141, 144, 147, 150, 154, 157, 160, 163, 167, 170, 173, 176, 179, 182, 185, 188, 191, 194, 197, 200, 202, 205, 208, 210, 213, 215, 217, 220, 222, 224, 226, 229,
    231, 232, 234, 236, 238, 239, 241, 242, 244, 245, 246, 248, 249, 250, 251, 251, 252, 253, 253, 254, 254, 255, 255, 255, 255, 255, 255, 255, 254, 254, 253, 253,
    252, 251, 251, 250, 249, 248, 246, 245, 244, 242, 241, 239, 238, 236, 234, 232, 231, 229, 226, 224, 222, 220, 217, 215, 213, 210, 208, 205, 202, 200, 197, 194,
    191, 188, 185, 182, 179, 176, 173, 170, 167, 163, 160, 157, 154, 150, 147, 144, 141, 137, 134, 131, 127, 124, 121, 117, 114, 111, 107, 104, 101, 97, 94, 91,
     88, 85, 81, 78, 75, 72, 69, 66, 63, 60, 58, 55, 52, 49, 47, 44, 42, 39, 37, 35, 32, 30, 28, 26, 24, 22, 20, 18, 17, 15, 13, 12,
     11, 9, 8, 7, 6, 5, 4, 3, 2, 2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
      0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };

}


