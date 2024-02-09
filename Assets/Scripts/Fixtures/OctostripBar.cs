using Scripts.BaseFixtures;


namespace Scripts.Fixtures
{
    public enum OctostripBarMapping
    {
        Rainbow = 0,
        Red,
        Green,
        Blue,
        Strobe,  // 1-20 Hz
        Chase  // sound active 241-255
    }
    
    public class OctostripBar : BaseFixtureSimpleRGB<OctostripBarMapping>
    {
        protected override void MapFromChannels()
        {
            // TODO: implement strobe
            color.r = channels[(int)OctostripBarMapping.Red] / 255.0f;
            color.g = channels[(int)OctostripBarMapping.Green] / 255.0f;
            color.b = channels[(int)OctostripBarMapping.Blue] / 255.0f;
        }
    }
}
