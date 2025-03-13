using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
 public CrowdManager crowdManager;
 
 private int _totalCoin=0;
 [SerializeField] private TextMeshProUGUI coinText,cubeBarLevelText,cubeBarLevelText2;
 [SerializeField]private Slider cubeBarSlider;
 [SerializeField]private ParticleSystem cubeBarSliderParticle;
 [SerializeField] private float expLimit;
 private float _cubeBarExp;
 private float _cubeLevel=1;

 private void Start()
 {
     //playerpreften alınacak
     coinText.text = _totalCoin.ToString();
     GameEventManager.Instance.OnCoinEarned += CoinEared;
     GameEventManager.Instance.OnCubeExpEarned += CubeExpEared;
     GameEventManager.Instance.OnLevelRestart += Restart;
 }

 private void CoinEared(int value)
 {
     _totalCoin += value;
     coinText.text = _totalCoin.ToString();

 }
 private void CubeExpEared(float value)
 {
     _cubeBarExp += value;
     cubeBarSliderParticle.Play();
     if (_cubeBarExp>=expLimit)
     {
         _cubeBarExp -= expLimit;
         _cubeLevel++;
         var nextLevel = _cubeLevel + 1;
         cubeBarLevelText.text = _cubeLevel.ToString();
         cubeBarLevelText2.text = nextLevel.ToString();

     }

     cubeBarSlider.value = _cubeBarExp / expLimit;
     coinText.text = _totalCoin.ToString();

 }

 private void Restart()
 {
     _cubeLevel=1;
     var nextLevel = _cubeLevel + 1;
     cubeBarLevelText.text = _cubeLevel.ToString();
     cubeBarLevelText2.text = nextLevel.ToString();
 }

}
