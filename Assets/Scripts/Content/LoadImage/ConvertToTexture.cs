using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ConvertToTexture : MonoBehaviour
{
    public static IEnumerator Convert(string imageLink, Action<Texture2D> callback)
    {
        string imageFilesPathC = imageLink;
        var readingTaskInFileC = File.ReadAllBytes(imageFilesPathC);

        Texture2D imageTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);


        imageTexture.LoadImage(readingTaskInFileC);
      
        if (imageTexture.width % 4 != 0 || imageTexture.height % 4 != 0)// если при делении на 4 высоты или ширины текстуры есть остаток
        {
            int sizeXShadersInConvertToTexture = imageTexture.width - imageTexture.width % 4;//отнимаем остаток от ширины
            int sizeYShaders = imageTexture.height - imageTexture.height % 4;//отнимаем остаток от высоты
            var newPixelsInImageTexture = imageTexture.GetPixels(0, 0, sizeXShadersInConvertToTexture, sizeYShaders);//сохраняем пиксели текстуры размером кратным 4
            imageTexture.Reinitialize(sizeXShadersInConvertToTexture, sizeYShaders);//меняем размер текстуры кратным 4
            imageTexture.SetPixels(newPixelsInImageTexture);//перезаписываем пиксели
            imageTexture.Apply(false, false);//сохраняем изменения
        }
      
        imageTexture.Apply(false, false);
    //Полученную сжатую текстуру можем передавать в коллбек и использовать в своих целях.

    callback?.Invoke(imageTexture);
        yield return callback;
    }
}
