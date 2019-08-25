using Terraria;
using Terraria.Graphics.Shaders;

namespace NMIP.Backgrounds
{
    public class ToxinSkyData : ScreenShaderData
    {
        public ToxinSkyData(string passName) : base(passName)
        {
        }

        private void UpdatetToxinSky()
        {
            NMIPPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<NMIPPlayer>();
            if (NMIPWorld.toxinTiles < 100)
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdatetToxinSky();
            base.Apply();
        }
    }
}