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
    public class PawnRenderNodeWorker_CHBody : PawnRenderNodeWorker_Body {
        public override float LayerFor(PawnRenderNode node, PawnDrawParms parms) {
            //参照するhediffのnullチェック
            var nodeHediff = node as PawnRenderNode_CosmeticHediff;
            var comp = nodeHediff.hediffComp;
            if (nodeHediff == null || comp == null) {
                return base.LayerFor(node, parms);
            }
            //兄弟レイヤーを見て...
            var childrenLayer = node.parent.children.Select(t => t == node ? node.parent.Props.baseLayer : t.Props.baseLayer);
            //...最前列または最後列に移動
            float result = comp.PropsCosmetic.frontRow ?
                childrenLayer.Max() + 1 :
                childrenLayer.Min() + 1;
            //デバッグメニューからの編集内容を忘れずに反映させよう
            return result + node.debugLayerOffset;
        }

    }
}
