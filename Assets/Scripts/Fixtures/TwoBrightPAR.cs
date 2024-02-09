

namespace Scripts
{
    public enum TwoBrightPARMapping
    {
        Red = 0,
        Green,
        Blue,
        White,
        Amber, 
        UV
    }

    public class TwoBrightPAR : BaseFixtureSimpleRGB<TwoBrightPARMapping>
    {
        protected override void MapFromChannels()
        {
            // TODO: implement White, Amber, UV
            color.r = channels[(int)TwoBrightPARMapping.Red] / 255.0f;
            color.g = channels[(int)TwoBrightPARMapping.Green] / 255.0f;
            color.b = channels[(int)TwoBrightPARMapping.Blue] / 255.0f;
        }
    }
}
