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
    //やってる事はPawnRenderNodeWorker_CHBodyと同じなのでそっち参照してちょ
    public class PawnRenderNodeWorker_CHHead : PawnRenderNodeWorker_Apparel_Head {
        public override float LayerFor(PawnRenderNode node, PawnDrawParms parms) {
            var nodeHediff = node as PawnRenderNode_CosmeticHediff;
            var comp = nodeHediff.hediffComp;
            if (nodeHediff == null || comp == null) {
                return base.LayerFor(node, parms);
            }
            var childrenLayer = node.parent.children.Select(t => t == node ? node.parent.Props.baseLayer : t.Props.baseLayer);
            float result = comp.PropsCosmetic.frontRow ?
                childrenLayer.Max() + 1 :
                childrenLayer.Min() + 1;
            return result + node.debugLayerOffset;
        }
    }
}
