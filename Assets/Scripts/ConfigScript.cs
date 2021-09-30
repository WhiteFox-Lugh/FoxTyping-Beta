using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConfigScript : MonoBehaviour {
	public static int Tasks {
		set;
		get;
	} = -1;

	// ゲームモード
	// 0 : 短文
	// 1 : 長文
	public static int GameMode {
		set;
		get;
	} = 0;

	// 画面中部に表示するもの
	// 0 : タイピングパフォーマンス情報
	// 1 : アシストキーボード
	public static int InfoPanelMode {
		set;
		get;
	} = 0;

	// 短文打つモードでのデータセットのファイル名
	public static string DataSetName {
		set;
		get;
	} = "FoxTypingOfficial";

	// 長文打つモードでのデータセットのファイル名
	public static string LongSentenceTaskName {
		set;
		get;
	} = "Long_Constitution";

	// 長文モードでの制限時間
	public static int LongSentenceTimeLimit {
		set;
		get;
	} = 300;

	// CPU Speed Guide の利用をするか
	public static bool UseCPUGuide {
		set;
		get;
	} = true;

	// CPU の kpm 設定
	public static int CPUKpm {
		set;
		get;
	} = 300;
}
