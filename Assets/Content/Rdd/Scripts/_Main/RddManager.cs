using System;
using System.Collections;
using Cf.Pattern;
using Rdd.CfSteam;
using Rdd.CfUi;
using UnityEngine;

namespace Rdd
{
    public class RddManager : Singleton<RddManager>
    {
        private SteamManager _mSteamManager;
        private UIManager _mUIManager;

        /// <summary>
        /// 시작 하자 자기 자신 바로 호출
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init()
        {
            _ = Instance;
        }

        /// <summary>
        /// 기초 라이브러리 로딩
        /// 방 목록 초기화
        /// </summary>
        /// <returns></returns>
        private IEnumerator Start()
        {
            yield return CoLoadUi();
            yield return CoLoadSteam();
        }

        /// <summary>
        /// Ui 라이브러리 로딩
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoLoadUi()
        {
            while (!UIManager.Instance)
            {
                yield return null;
            }

            _mUIManager = UIManager.Instance;
        }

        /// <summary>
        /// Steam 라이브러리 로딩
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoLoadSteam()
        {
            while (!SteamManager.Instance)
            {
                yield return null;
            }

            _mSteamManager = SteamManager.Instance;
        }
    }
}
