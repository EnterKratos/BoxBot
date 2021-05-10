﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    PreLoad = 0,
    MainMenu = 1,
    Level1 = 2
}

public static class SceneHelpers
{
    public static IEnumerator LoadSceneInBackground(Scene scene, Func<bool> waitUntil)
    {
        yield return null;

        var asyncOperation = SceneManager.LoadSceneAsync((int)scene);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Activation held until fully loaded and flag set
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitUntil(waitUntil);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}