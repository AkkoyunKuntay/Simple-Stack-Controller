using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StackType { Vertical, Horizontal }

public class StackController : MonoBehaviour
{
    public static StackController Instance;

    [Header("Stack Variables")]
    public List<Transform> StackList = new List<Transform>();
    private List<Vector3> displacements = new List<Vector3>();
    public int stackAmount;
    public Transform previousObject;
    public Transform lastObject;
    public Transform stackParent;

    [Header("Stack Settings")]
    public float zGapBetweenStacks = 1.25f;
    public float displacementSpeed = 6;
    private Vector3 DesiredPos;
    private Quaternion DesiredRot;

    [Header("Stack Type")]
    public StackType stackType;
    public bool ReverseAxis;
    Vector3 stackDirection;

    private void OnValidate()
    {
        switch (stackType)
        {
            case StackType.Horizontal:
                if (!ReverseAxis)
                {
                    stackDirection = Vector3.forward;
                }
                else
                {
                    stackDirection = -Vector3.forward;
                }  
                break;

            case StackType.Vertical:
                if (!ReverseAxis)
                {
                    stackDirection = Vector3.up;
                }
                else
                {
                    stackDirection = -Vector3.up;
                }
                break;

            default:
                break;
        }
    }



    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        DesiredPos = stackParent.transform.localPosition;
        DesiredRot = stackParent.transform.rotation;
    }


    private void Update()
    {

        stackAmount = StackList.Count;
        CalculateStackDeflection();
    }

    public void PickUpItem(Transform pickedObject)
    {
        displacements.Add(Vector3.zero);
        StackList.Add(pickedObject);
        previousObject = lastObject;
        lastObject = pickedObject;
        pickedObject.tag = "CollectedObject";
    }
    public Transform RemoveLastObject(Transform targetObject)
    {
        if (stackAmount == 0)
        {
            return null;
        }
        int index = StackList.IndexOf(targetObject);
        displacements.RemoveAt(index);

        var lastObj = targetObject;
        StackList.Remove(lastObj);
        var go = lastObj;

        return go;
    }
    public void CalculateStackDeflection()
    {
        Vector3 stackPos = stackParent.position;

        for (int i = 0; i < StackList.Count; i++)
        {
            Transform child = StackList[i];
            Vector3 targetPos; // stackparenta ya da ilk elemana göre belirlediğimiz virtual bir nokta.

            if (stackType == StackType.Horizontal)
            {
                if (i == 0)
                {
                    targetPos = stackPos + stackDirection * 1.25f;  // ilk stack elemanının benden uzaklığı
                }

                else
                {
                    targetPos = StackList[i - 1].position + zGapBetweenStacks * stackDirection; // bir önceki elemanın pozisyonuna olan uzaklığı (Aralık)
                }

                Vector3 displacement = displacementSpeed * Time.deltaTime * (targetPos - child.position); //Stack elemanlarının yer değiştirmesi    

                displacement += ((.75f) * displacements[i]); // Önceki displacement vector'ünün 0.25 ini yiyerek yeni vektörün üstüne ekledik (Damper FX)

                // Y ve Z ekseninde yer değiştirme yapmak istemiyoruz dolayısıyla sıfırlıyoruz.
                displacement.y = 0;
                displacement.z = 0;

                Vector3 pos = child.position + displacement; // stack elemanının pozisyonuna yer değiştirme vektörümüzü ekledik.
                pos.z = targetPos.z; // Z ekseninde herhangi bir kayma olmasını engellemek için stack parent ın Z eksenini pos.z ye verdik.

                child.position = pos; // stack elemanının pozisyonunu yukarıda oluşturduğumuz yukarıda oluşturduğumuz pos değişkenine eşitledik.
                displacements[i] = displacement; // stack elemanının yer değiştirme vektörünü kaydetmiş olduk.



            }
            else
            {
                if (i == 0)
                {
                    targetPos = stackPos + stackDirection * 1.25f;  // ilk stack elemanının benden uzaklığı
                }

                else
                {
                    targetPos = StackList[i - 1].position + zGapBetweenStacks * stackDirection; // bir önceki elemanın pozisyonuna olan uzaklığı (Aralık)
                }

                Vector3 displacement = displacementSpeed * Time.deltaTime * (targetPos - child.position); //Stack elemanlarının yer değiştirmesi    

                displacement += ((.75f) * displacements[i]); // Önceki displacement vector'ünün 0.25 ini yiyerek yeni vektörün üstüne ekledik (Damper FX)

                // Y ve Z ekseninde yer değiştirme yapmak istemiyoruz dolayısıyla sıfırlıyoruz.
                //displacement.x = 0;
                displacement.z = 0;

                Vector3 pos = child.position + displacement; // stack elemanının pozisyonuna yer değiştirme vektörümüzü ekledik.
                pos.z = targetPos.z; // Z ekseninde herhangi bir kayma olmasını engellemek için stack parent ın Z eksenini pos.z ye verdik.

                child.position = pos; // stack elemanının pozisyonunu yukarıda oluşturduğumuz yukarıda oluşturduğumuz pos değişkenine eşitledik.
                displacements[i] = displacement; // stack elemanının yer değiştirme vektörünü kaydetmiş olduk.
            }
        }
    }
}
