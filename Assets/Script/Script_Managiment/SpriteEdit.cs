using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;

// 作り方動画
// https://www.youtube.com/watch?v=ZgUadOvvjrA

public class SpriteEdit : MonoBehaviour
{
    SpriteRenderer s_renderer = new SpriteRenderer();

    Vector2 rad = new Vector2(30000, 30000);
    Vector2 maskrad = new Vector2(10000, 10000);
    Rect rect ;
    Rect maskrect ;

    public Vector2 position = Vector2.zero;

    private void Start()
    {
        s_renderer = this.GetComponent<SpriteRenderer>();
        rect = new Rect();
        maskrect = new Rect();
        //rect = this.gameObject.GetComponent<Rect>();
        
    }

    private void Update()
    {
        BuiidSprite();
    }




    void BuiidSprite()
    {
        //ベジエ曲選の情報を詰めるシェイプ定義
        var shape = new Shape();
        
        //####################################################
        //シェイプに円作成に必要な情報を詰める
        //var position = Vector2.zero;
        float radius = 10000f;
        int i;
        for(i = 0; i < 2; i++)
        {
            
        }

        shape.Contours = BuildCircleContourWithMask(rect, rad, maskrect, maskrad);
        VectorUtils.MakeCircleShape(shape, position, radius);
        
        //円の色設定
        shape.Fill = new SolidFill() { Color = Color.green, Mode = FillMode.OddEven }; //中くりぬきの円
        //shape.Fill = new SolidFill() { Color = Color.green }; //通常の円
        //####################################################



        //円の手ッセレーションオプションを決める
        var options = new VectorUtils.TessellationOptions()
        {
            StepDistance = 10f,
            MaxCordDeviation = float.MaxValue,
            MaxTanAngleDeviation = Mathf.PI / 2.0f,
            SamplingStepSize = 0.01f

        };



        //ジオメトリの作成
        var node = new SceneNode()
        {
            Shapes = new List<Shape> { shape }
        };

        var scene = new Scene() { Root = node };
        var geoms = VectorUtils.TessellateScene(scene, options);



        //Spriteの作成と設定
        var sprite = VectorUtils.BuildSprite
            (
            geoms, 100.0f,
            VectorUtils.Alignment.Center,
            Vector2.zero, 128
            );
        
        s_renderer.sprite = sprite;
        //Debug.Log(sprite);
    }

    //これを読ませたい
    private BezierContour[] BuildCircleContourWithMask(Rect rect, Vector2 rad, Rect maskRect, Vector2 maskRad)
    {
        var contours = new BezierContour[2];
        contours[0] = VectorUtils.BuildRectangleContour(rect, rad, rad, rad, rad);
        contours[1] = VectorUtils.BuildRectangleContour(maskRect, maskRad, maskRad, maskRad, maskRad);

       
        return contours; //BezierContour[]を返す

    }


}
