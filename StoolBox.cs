using UnityEngine;

public class StoolBox : Box
{
    protected override void OpenBox()
    {
        base.OpenBox();

        TutorialManager.Instance.stoolTop = GameObject.Find("StoolTop(Clone)").GetComponent<IBlinking>();
        TutorialManager.Instance.stoolLeg = GameObject.Find("StoolLeg(Clone)").GetComponent<IBlinking>();

        TutorialManager.Instance.MakeStoolPartsBlink();
    }
}
