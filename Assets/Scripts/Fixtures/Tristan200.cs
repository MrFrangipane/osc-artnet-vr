using System.Collections.Generic;
using Scripts.BaseFixtures;
using UnityEngine;


namespace Scripts.Fixtures
{
    public enum Tristan200Mapping
    {
        Pan = 0,  // 540°
        PanFine, 
        Tilt,  // 270°
        TiltFine, 
        MovingSpeed,  // decreasing 
        Color, 
        Gobo1, 
        Gobo1Rotation, 
        Gobo2, 
        Prism,  // 08-255 
        PrismRotation, 
        Frost, 
        Focus, 
        FocusFine, 
        Shutter, 
        Dimmer, 
        DimmerFine, 
        Reset // 128-255
    }

    public class Tristan200 : BaseFixtureMovingHead<Tristan200Mapping>
    {
        protected override void MapChannels()
        {
            pan = (channels[(int)Tristan200Mapping.Pan] / 255.0f * 540) - 270;
            tilt = (channels[(int)Tristan200Mapping.Tilt] / 255.0f * 270) - 135;

            var colorWheel = new Dictionary<int, float> {
                { 66, 0f },
                { 67, 0.04f },
                { 68, 0.06f },
                { 69, 0.09f },
                { 70, 0.12f },
                { 71, 0.18f },
                { 72, 0.30f },
                { 78, 0.46f },
                { 74, 0.63f },
                { 80, 0.84f }
            };

            var hue = 0.0f;
            if (colorWheel.ContainsKey(channels[(int)Tristan200Mapping.Color]))
            {
                hue = colorWheel[channels[(int)Tristan200Mapping.Color]];
            }
            
            color = Color.HSVToRGB(
                hue,
                (channels[(int)Tristan200Mapping.Color] == 64) ? 0f: 1f, // color 64 is white 
                channels[(int)Tristan200Mapping.Dimmer] / 255.0f
            );
        }
    }
}
