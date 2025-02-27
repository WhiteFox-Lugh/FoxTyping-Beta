using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using Assert = UnityEngine.Assertions.Assert;

public class LongSentenceScriptTest
{
    private enum judgeType {
        insert,
        delete,
        replace,
        correct
    };
    const string FILE_CONSTITUTION = "long_constitution";
    const string COLORFUL_MAKIGAMI = "あかまきがみあおまきがみきまきがみ";

    // Diff チェック関数
    private void DiffChecker(string taskStr, string inputStr, List<Diff> expectedDiffs){
        var lsClass = typeof(LongSentenceScript);
        Assert.IsNotNull(lsClass);
        MethodInfo diffParser = lsClass.GetMethod("GetDiff", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.IsNotNull(diffParser);

        // diff 取得
        var diffList = (List<Diff>) diffParser.Invoke(this, new object[] {taskStr, inputStr});

        // debug 用
        for (int i = 0; i < diffList.Count; ++i){
            var actual = diffList[i];
            Debug.Log("[" + actual.op + ", " + actual.before + ", " + actual.after + "]");
        }
        Assert.AreEqual(diffList.Count, expectedDiffs.Count);

        // 個別に diff をチェック
        for (int i = 0; i < diffList.Count; ++i){
            var expected = expectedDiffs[i];
            var actual = diffList[i];
            Debug.Log("op : [" + expected.op + ", " + actual.op + "]");
            Debug.Log("before : [" + expected.before + ", " + actual.before + "]");
            Debug.Log("after : [" + expected.after + ", " + actual.after + "]");
            Assert.IsTrue((expected.op).Equals(actual.op));
            Assert.IsTrue((expected.before).Equals(actual.before));
            Assert.IsTrue((expected.after).Equals(actual.after));
        }
    }

    private string LoadTaskData(string fileName){
        var cls = typeof(LongSentenceScript);
        Assert.IsNotNull(cls);
        MethodInfo loader = cls.GetMethod("LoadSentenceData", BindingFlags.NonPublic | BindingFlags.Static);
        var ret = (string) loader.Invoke(this, new object[] {fileName});
        return ret;
    }

    [Test]
    [Category("CompleteMatch")]
    public void CompleteMatchTest01() {
        var taskStr = "きつね";
        var inputStr = "きつね";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "きつね", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("PrefixMatch")]
    public void PrefixMatchTest01() {
        var taskStr = "きつねこんこん";
        var inputStr = "きつね";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "きつね", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("SuffixMatch")]
    public void SuffixMatchTest01() {
        var taskStr = "きつねこんこん";
        var inputStr = "こんこん";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.delete, "きつね", ""),
            new Diff((int)judgeType.correct, "こんこん", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("SuffixMatch")]
    public void SuffixMatchTest02() {
        var taskStr = "きつねこんこん";
        var inputStr = "んこん";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.insert, "", "んこん")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("SuffixMatch")]
    public void SuffixMatchTest03() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "まきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.delete, "あか", ""),
            new Diff((int)judgeType.correct, "まきがみ", ""),
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("MiddleMatch")]
    public void MiddleMatchTest01() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "あおまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "あ", ""),
            new Diff((int)judgeType.insert, "", "おまきがみ"),
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("MiddleMatch")]
    public void MiddleMatchTest02() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "あかあおまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "あか", ""),
            new Diff((int)judgeType.delete, "まきがみ", ""),
            new Diff((int)judgeType.correct, "あおまきがみ", ""),
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void NoneInputTest() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void NotMatchTest() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "いろはにほへと";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.insert, "", "いろはにほへと")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void OvertypingTest01() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = COLORFUL_MAKIGAMI + "あ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, COLORFUL_MAKIGAMI, ""),
            new Diff((int)judgeType.insert, "", "あ")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void OvertypingTest02() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "あかまきがみあおまきがみあかまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "あかまきがみあおまきがみ", ""),
            new Diff((int)judgeType.replace, "き", "あ"),
            new Diff((int)judgeType.insert, "", "か"),
            new Diff((int)judgeType.correct, "まきがみ", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void SuffixMultiMatchTest01() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "まきがみまきがみまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.replace, "あか", "まき"),
            new Diff((int)judgeType.insert, "", "がみ"),
            new Diff((int)judgeType.correct, "まきがみ", ""),
            new Diff((int)judgeType.delete, "あお", ""),
            new Diff((int)judgeType.correct, "まきがみ", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("ExtremeCases")]
    public void SuffixMultiMatchTest02() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "まきがみまきがみまきがみまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.replace, "あか", "まき"),
            new Diff((int)judgeType.insert, "", "がみ"),
            new Diff((int)judgeType.correct, "まきがみ", ""),
            new Diff((int)judgeType.delete, "あお", ""),
            new Diff((int)judgeType.correct, "まきがみ", ""),
            new Diff((int)judgeType.delete, "き", ""),
            new Diff((int)judgeType.correct, "まきがみ", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }

    [Test]
    [Category("OrdinalCases")]
    public void OrdinalCaseTest01() {
        var taskStr = COLORFUL_MAKIGAMI;
        var inputStr = "あかまきがみあまきがみ";
        var expectedDiffs = new List<Diff> () {
            new Diff((int)judgeType.correct, "あかまきがみあ", ""),
            new Diff((int)judgeType.delete, "お", ""),
            new Diff((int)judgeType.correct, "まきがみ", "")
        };
        DiffChecker(taskStr, inputStr, expectedDiffs);
    }
}
