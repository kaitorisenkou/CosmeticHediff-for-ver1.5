using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace CosmeticHediff_v15 {
    public class HediffComp_Cosmetic : HediffComp {
        public HediffCompProperties_Cosmetic PropsCosmetic {
            get {
                return this.props as HediffCompProperties_Cosmetic;
            }
        }
        Graphic graphic = null;
        public Graphic GetGraphic() {
            if (graphic == null)
                ResolveGraphic();
            return graphic;
        }

        void ResolveGraphic() {
            string path = PropsCosmetic.graphicPath;
            if (PropsCosmetic.useBodytype) {
                BodyTypeDef bodyType = Pawn.story.bodyType ?? BodyTypeDefOf.Male;
                path += "_" + bodyType.defName;
            }
            Shader shader = PropsCosmetic.useWornGraphicMask ? ShaderDatabase.CutoutComplex : ShaderDatabase.Cutout;
            Vector2 drawSize = PropsCosmetic.drawSize;
            Color color = PropsCosmetic.color;

            this.graphic = GraphicDatabase.Get<Graphic_Multi>(path, shader, drawSize, color);
        }

        public override void CompPostMake() {
            //↓レンダーツリーのキャッシュを更新するにはコレ
            Pawn.Drawer.renderer.SetAllGraphicsDirty();
            base.CompPostMake();
        }
        public override void CompPostPostRemoved() {
            Pawn.Drawer.renderer.SetAllGraphicsDirty();
            base.CompPostPostRemoved();
        }

    }
    public class HediffCompProperties_Cosmetic : HediffCompProperties {
        public HediffCompProperties_Cosmetic() {
            this.compClass = typeof(HediffComp_Cosmetic);
        }

        //public CosmeticHediffLayer layer = CosmeticHediffLayer.apparel;
        public bool useBodytype = true;
        public Type workerClass = typeof(PawnRenderNodeWorker_CHBody);
        public bool frontRow = true;
        public bool useWornGraphicMask = false;
        public Vector2 drawSize = Vector2.one;
        public Color color = Color.white;
        [NoTranslate]
        public string graphicPath = "";
        public PawnRenderNodeTagDef parentTagDef = PawnRenderNodeTagDefOf.ApparelBody;
        //public List<RenderSkipFlagDef> renderSkipFlags;
        public DrawData drawData;
    }
}
