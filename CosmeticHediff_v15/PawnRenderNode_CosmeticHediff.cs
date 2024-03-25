using RimWorld;
using Verse;

namespace CosmeticHediff_v15 {
    public class PawnRenderNode_CosmeticHediff : PawnRenderNode {
        public HediffComp_Cosmetic hediffComp;
        public PawnRenderNode_CosmeticHediff(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree, HediffComp_Cosmetic hediffComp = null) : base(pawn, props, tree) {
            this.hediffComp = hediffComp;
        }

        protected override void EnsureMaterialsInitialized() {
            if (this.graphic == null && hediffComp!=null) {
                this.graphic = hediffComp.GetGraphic();
            }
        }

    }
}
