using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Verse;
using RimWorld;

namespace CosmeticHediff_v15 {
    public class CompCosmeticHediff :ThingComp {
        //この関数をオーバーライドすると衣服とかと同タイミングでレンダーツリーにノードが追加される
        public override List<PawnRenderNode> CompRenderNodes() {
            Pawn pawn = parent as Pawn;
            if (pawn == null) {
                return base.CompRenderNodes();
            }
            //反復子使いたかったのでそっちへGoGo
            return RenderNodesInternal(pawn).ToList();
        }

        IEnumerable<PawnRenderNode> RenderNodesInternal(Pawn pawn) {
            Dictionary<PawnRenderNodeTagDef, int> nums = new Dictionary<PawnRenderNodeTagDef, int>();
            foreach (var i in pawn.health.hediffSet.hediffs) {
                //ここからnullチェック
                var comp = i.TryGetComp<HediffComp_Cosmetic>();
                if (comp == null) {
#if DEBUG
                    Log.Message("[CHv15] no comp: " + i.def.defName);
#endif
                    continue;
                }
                //ここまでnullチェック

                HediffCompProperties_Cosmetic props = comp.PropsCosmetic;
                //ノードのプロパティを設定
                var pawnRenderNodeProperties = new PawnRenderNodeProperties {
                    //デバッグメニューにおける表示名(任意？)
                    debugLabel = i.def.defName,
                    //ノードの追加位置は基本的にルート直下だが、parentTagDefを設定すればそのタグを持つノードの子になる
                    parentTagDef = props.parentTagDef,
                    //どのノードの子になろうがworkerClass無しだとレイヤー0に配置されるのでほぼ必須
                    workerClass = props.workerClass,
                    //ブルカ(衣装)などに設定されてる特異な設定(任意？)
                    drawData = props.drawData
                };

                //↓これが返り値↓
                yield return new PawnRenderNode_CosmeticHediff(
                    //ツリーの持ち主
                    pawn,
                    //さっき定義したプロパティ
                    pawnRenderNodeProperties,
                    //ツリーそのもの
                    pawn.Drawer.renderer.renderTree,
                    //4つ目以降の引数は子クラスの特権
                    comp
                    );
                //↑これが返り値↑
#if DEBUG
                Log.Message("[CHv15] added: " + i.def.defName);
#endif
            }
            yield break;
        }
    }
    public class CompProperties_CosmeticHediff : CompProperties {
        public CompProperties_CosmeticHediff() {
            this.compClass = typeof(CompCosmeticHediff);
        }

    }
}
