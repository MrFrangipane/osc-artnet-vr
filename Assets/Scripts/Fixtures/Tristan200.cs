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

    public class Tristan200 : BaseFixtureMovingHead<OctostripBarMapping>
    {
        protected override void MapChannels()
        {
            pan = (channels[(int)Tristan200Mapping.Pan] / 255.0f * 540) - 270;
            tilt = (channels[(int)Tristan200Mapping.Tilt] / 255.0f * 270) - 135;

            color = Color.HSVToRGB(
                channels[(int)Tristan200Mapping.Color] / 255.0f,
                1f,
                1f // channels[(int)Tristan200Mapping.Dimmer] / 255.0f
            );
        }
    }
}
