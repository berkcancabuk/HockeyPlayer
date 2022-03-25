using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public Animator playeranim;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float speed;
    public Vector3 puckUpPos;
    public Transform firstScalePosRightX;
    public Transform firstScalePosLeftX;
    public Transform SecondScalePosRightX;
    public Transform SecondScalePosLeftX;
    //public float swerveAmount;
    //public float swerveSpeed = 0.3f;
    //public float _lastFrameFingerPositionX;
    //public float _moveFactorX;
    //public float maxSwerveAmount = 2f;
    //public float minSwerveAmount = -2f;
    bool right = false;
    bool left = false;
    public GameObject puck;
    [SerializeField] float puckTranslate;
    [SerializeField] GameObject starsAnim;
    [SerializeField] int scoreValue = 0;
    [SerializeField] Image colorBar;
    [SerializeField] Image scoreBar;
    public bool isFinish = false;
    public GameObject target;
    public GameObject stick;
    public Vector3 targetstart;
    //Sequence Sequence = DOTween.Sequence();
    //Sequence Sequence2 = DOTween.Sequence();
    //public int avgFrameRate;
    //public TextMeshProUGUI display_Text;

    [SerializeField] List<Vector3> LeftRightScale03List = new List<Vector3>();
    void Start()

    {
        for (int i = 1; i < 91; i++)
        {
            /*target.transform.localPosition*/
            LeftRightScale03List[i-1] = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.18f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
            
        }


        //targetstart = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        rightValue = Mathf.Clamp(rightValue, 0, 1);
        
        leftValue = Mathf.Clamp(leftValue, 0, 1); ;
        scoreValue = 0;
        playeranim = this.GetComponent<Animator>();
        print(Mathf.Sin(Mathf.Deg2Rad*30));
        print(Math.Cos(2));
        //StartCoroutine(LEFTRÝGHT());
        
    }
    void Update()
    {
        //float current = 0;
        //current = Time.frameCount / Time.time;
        //avgFrameRate = (int)current;
       
        //display_Text.text = avgFrameRate.ToString() + " FPS";

        this.transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -3, 3), transform.localPosition.y, transform.localPosition.z);
        speed = Mathf.Clamp(speed, 5, 10);
        //transform.Translate(new Vector3(0, 0, Time.fixedDeltaTime * speed));
        if (isFinish == false)
        {
            Swipe();
            transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
           
            if (right == true)
            {

                transform.Translate(new Vector3(Time.deltaTime * 5, 0, 0));
                //puck.transform.localPosition = new Vector3(0.31f, puck.transform.localPosition.y, puck.transform.localPosition.z);
            }
            if (left == true)
            {
                transform.Translate(new Vector3(Time.deltaTime * (-5), 0, 0));
                //puck.transform.localPosition = new Vector3(-0.2f, puck.transform.localPosition.y, puck.transform.localPosition.z);
            }
        }
        //stick.transform.position = target.transform.position;
        //target.transform.position = stick.transform.position;
        //hips.transform.rotation = Quaternion.Euler(61.93f, -89.7166f, -36.4969f);
        //this.transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -3, 3), transform.localPosition.y, transform.localPosition.z);
        //speed = Mathf.Clamp(speed, 5, 10);


        //rightValue = Mathf.Clamp(rightValue, 0, 1);
        //leftValue = Mathf.Clamp(leftValue, 0, 1); ;
        //StartCoroutine(SpeedDown());


        //swerveAmount = Time.fixedDeltaTime * swerveSpeed * _moveFactorX;
        //swerveAmount = Mathf.Clamp(swerveAmount, minSwerveAmount, maxSwerveAmount);
        //this.transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -4, 4), transform.localPosition.y, transform.localPosition.z);
        //transform.Translate(swerveAmount, 0, 0);
        print(leftToLeft);
    }

    

        //Swipe();
    
    public float leftValue = 0;
    public float rightValue = 0;
    public bool isSpeedDown = false;
    public bool isLeftMove = false;
    public bool rightSwipe = false;
    public bool leftSwipe = false;
    public void Swipe()
    {
        //StartCoroutine(SpeedDown());
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                //_lastFrameFingerPositionX = Input.mousePosition.x;
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            else if ((t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
            {
                //_moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;

                //_lastFrameFingerPositionX = Input.mousePosition.x;
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (currentSwipe.x < 0 /*&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f*/)
                {
                    right = false;
                    left = true;
                    playeranim.SetBool("RightHand", false);
                    playeranim.SetBool("LeftHand", true);
                }
                //swipe right
                if (currentSwipe.x > 0 /*&& currentSwipe.y > -0.2f && currentSwipe.y < 0.1f*/)
                {
                    left = false;
                    right = true;

                    playeranim.SetBool("RightHand", true);
                    playeranim.SetBool("LeftHand", false);
                }

                //else
                //{
                //    playeranim.SetBool("RightHand", false);
                //    playeranim.SetBool("LeftHand", false);
                //}
                currentSwipe.Set(0, 0);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                //_moveFactorX = 0f;
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (currentSwipe.x < 0 /*&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f*/)
                {
                    leftSwipe = true;
                    rightSwipe = false;
                    rightleftTurn = 0;
                    rightToRight = 0;
                    leftToLeft++;
                    rightToIdles = false;
                    leftrightTurn = 0;
                    playeranim.SetBool("RightHand", true);
                    playeranim.SetBool("LeftHand", false);
                    CancelInvoke("LeftToIdle");
                    isLeftSwipe = true;
                    print("x 0 dan küçük");
                    if (puck.transform.localScale.x <= 0.3)
                    {
                        
                        StartCoroutine(RÝGHTLEFT03scale());

                        //isLeftMove = false;
                        //target.transform.DOLocalMove(new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, +0.01f), 0.3f).OnComplete(() => target.transform.DOLocalMove(new Vector3(target.transform.localPosition.x, -0.04f, +0.097f), 0.3f));
                        //for (float i = 0.0022f; i < 4; i+= 0.0022f)
                        //{
                        //    target.transform.localPosition = new Vector3(Mathf.Cos(((2 * i) * (2/10))), Mathf.Sin(((2 * i) * (2 / 10))), 0.4867598f);
                        //}

                        //DOTween.Kill(target);
                        //print(DOTween.Kill(target));
                        //target.transform.DOLocalMove(new Vector3(0.08f, 0.65f, 0.4867598f), 0.2f).SetTarget("yukarýcikis").SetEase(Ease.Linear).OnComplete(() => target.transform.DOLocalMove(new Vector3(-0.17f,0.623f,0.4867f),0.2f)).OnComplete(() => target.transform.DOLocalMove(new Vector3(-0.022f, 0.623f, 0.48f), 0.2f));
                    }
                    if (puck.transform.localScale.x > 0.3 && puck.transform.localScale.x <= 0.5)
                    {
                        StartCoroutine(RÝGHTLEFT05scale());
                        //target.transform.DOLocalMove(new Vector3(-0.13f, 0.663f, 0.441f), 1f);
                    }
                    if (puck.transform.localScale.x > 0.5 && puck.transform.localScale.x <= 0.9)
                    {
                        StartCoroutine(RÝGHTLEFT09scale());
                        print("giriyoz mu");
                        //target.transform.DOLocalMove(new Vector3(-0.371f, 0.678f, 0.368f), 1f);
                    }
                    isSpeedDown = false;
                    right = false;
                    left = true;
                    //playeranim.SetBool("RtoL", false);
                    leftValue++;
                    print(leftValue);
                }
                else
                {
                    Invoke("RightToIdLe", 1);

                }

                //swipe right
                if (currentSwipe.x > 0 /*&& currentSwipe.y > -0.2f && currentSwipe.y < 0.1f*/)
                {
                    print("x büyük");

                    rightSwipe = true;
                    leftSwipe = false;
                    leftToLeft = 0;
                    print("x 0 dan büyük");
                    rightToRight++;
                    LeftToIdles = false;
                    playeranim.SetBool("LeftHand", true);
                    playeranim.SetBool("RightHand", false);
                    CancelInvoke("RightToIdLe");
                    isRightSwipe = true;
                    //playeranim.SetBool("LeftHand", false);

                    //rightToIdles = false;

                    //StartCoroutine(RightToIdLe());
                    if (puck.transform.localScale.x <= 0.3)
                    {
                        
                        StartCoroutine(LEFTRÝGHT03scale());
                        //target.transform.position = new Vector3(0, 0, 0);
                        //target.transform.DOLocalMove(new Vector3(-0.053f, -0.148f, 0.103f), 0.3f).OnComplete(()=> target.transform.DOLocalMove(new Vector3(0,0,0),0.7f));
                        //target.transform.localPosition = targetstart;
                        //target.transform.DOLocalMove(new Vector3(-0.039f, -0.169f, 0.124f), 0.3f).OnComplete(()=> print("asd")).OnComplete(() => target.transform.DOLocalMove(new Vector3(-0.143f, 0.073f, 0.01f), 0.3f)).OnComplete(() => target.transform.DOLocalMove(new Vector3(target.transform.localPosition.x, -0.04f, -0.1f), 0.3f).OnComplete(() => isLeftMove=true));

                        //if (isLeftMove == true)
                        //{
                        //    print("girdi mi ");
                        //    target.transform.DOLocalMove(new Vector3(-0.039f, -0.197f, -0.14f), 0.3f).OnComplete(() => target.transform.DOLocalMove(new Vector3(-0.143f, 0.073f, 0.01f), 0.3f)).OnComplete(() => target.transform.DOLocalMove(new Vector3(target.transform.localPosition.x, -0.04f, -0.1f), 0.3f));
                        //}



                        //    //StartCoroutine(LEFTRÝGHT());
                        //    //if (i >= 0.15)
                        //    //{
                        //    //    target.transform.position = new Vector3(target.transform.position.x+i, target.transform.position.y+0.2f, target.transform.position.z);
                        //    //}
                        //    //}
                        //    //DOTween.Kill("yukarýcikis", false);
                        //    //target.transform.DOLocalMove(new Vector3(0.08f, 0.9f, 0.4867598f), 0.2f).SetTarget("asagi").SetEase(Ease.Linear)/*.OnStepComplete(
                        //    //    () => target.transform.DOLocalMove(new Vector3(0.4f, 0.7f, 0.48f), 0.2f)).OnStepComplete(() => target.transform.DOLocalMove(new Vector3(1f,0.6f,0.48f),1f))*/.OnStepComplete(() => target.transform.DOLocalMove(new Vector3(0.2f, 0.6f, 0.48f), 0.2f));

                    }
                    if (puck.transform.localScale.x > 0.3 && puck.transform.localScale.x <= 0.5)
                    {
                        StartCoroutine(LEFTRÝGHT05scale());
                        //target.transform.DOLocalMove(new Vector3(0.426f, 0.457f, 0.683f), 1f);
                    }
                    if (puck.transform.localScale.x > 0.5 && puck.transform.localScale.x <= 0.9)
                    {
                        StartCoroutine(LEFTRÝGHT09scale());
                        //target.transform.DOLocalMove(new Vector3(0.398f, 0.467f, 0.67f), 1f);
                        //target.transform.localRotation = Quaternion.Euler(-157.648f, 86.834f, 6.852005f);
                    }

                    isSpeedDown = false;
                    left = false;
                    right = true;
                    //playeranim.SetBool("RtoL", true);
                    rightValue++;
                    print(rightValue);
                }
                else
                {
                    Invoke("LeftToIdle", 1);
                    

                }
                if (rightValue + rightValue == 2)
                {
                    isSpeedDown = false;
                    rightValue = 0;
                    leftValue = 0;
                    speed += 0.5f;

                }
                else
                {
                    isSpeedDown = true;
                }
                Invoke("RightToIdLe", 1);
                
            }
           
        }
    }
    public bool rightToIdles = false;
    public bool LeftToIdles = false;
    void RightToIdLe()
    {
        rightToIdles = true;
        if (rightToIdles == true)
        {
            playeranim.SetBool("LeftHand", false);
        }
    }
    void LeftToIdle()
    {
        LeftToIdles = true;
        if (LeftToIdles == true)
        {
            print("rightand fasle");
            playeranim.SetBool("RightHand", false);
        }
    }
    public int rightleftTurn = 0;
    public int rightToRight = 0;
    public bool isLeftSwipe = false;
    IEnumerator RÝGHTLEFT03scale()
    {
        if (isLeftSwipe==true)
        {
           
            //target.transform.localRotation = Quaternion.Euler(-6.44f, 1.71f, 30.092f);
            if (leftToLeft <= 1)
            {
                for (int i = 1; i < 91; i++)
                {
                    //rightleftTurn++;
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.15f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
                    if (i == 15)
                    {
                        target.transform.localRotation = Quaternion.Euler(-6.44f, 1.71f, 11f);
                    }
                    //display_Text.text = "rightleft03scaledeki deðerleri" + target.transform.localPosition;
                    rightleftTurn = i;
                    yield return new WaitForSeconds(0.005f);
                    if (rightSwipe == true)
                    {
                        for (int j = rightleftTurn; 0 < j; j--)
                        {
                            print("break sonrasý girdimi");
                            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.15f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
                        }
                        break;

                    }
                }
            }
            if (leftToLeft >= 2)
            {
                //playeranim.SetInteger("LeftToLefts", 2);
                //playeranim.SetBool("LeftToIdle", false);
                for (int i = 0; i < 50; i++)
                {
                    target.transform.localPosition = new Vector3(Mathf.Cos((4 * i * Mathf.Deg2Rad)) * +0.1f, target.transform.localPosition.y, target.transform.localPosition.z);
                    yield return new WaitForSeconds(0.005f);
                }
                //yield return new WaitForSeconds(.3f);
                for (int i = 1; i < 91; i++)
                {
                    //rightleftTurn++;
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.15f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
                    if (i == 15)
                    {
                        target.transform.localRotation = Quaternion.Euler(-6.44f, 1.71f, 11f);
                    }
                    //display_Text.text = "rightleft03scaledeki tekrar sol yapýnca deðerleri"+ target.transform.localPosition;
                    //print(target.transform.localPosition + "  rightleft03scaledeki tekrar sol yapýnca deðerleri");
                    yield return new WaitForSeconds(0.005f);
                    if (rightSwipe == true)
                    {
                        break;
                    }
                }
            }
            rightleftTurn = 0;
            isLeftSwipe = false;
        }

    }
    IEnumerator RÝGHTLEFT05scale()
    {
       
        for (int i = 1; i < 91; i++)
        {
            //rightleftTurn++;
            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
            if (i==15)
            {
                target.transform.localRotation = Quaternion.Euler(-6.44f, 1.71f, 11f);
            }
            rightleftTurn = i;
            yield return new WaitForSeconds(0.000001f);
            if (rightSwipe == true)
            {
                for (int j = rightleftTurn; 0 < j; j--)
                {
                    print("break sonrasý girdimi");
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
                }
                break;

            }
        }
        //if (rightSwipe == true)
        //{
        //    for (int i = rightleftTurn; 0 < i; i--)
        //    {
        //        print("break sonrasý girdimi");
        //        target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
        //        yield return new WaitForSeconds(0.000001f);
        //    }
        //    print(rightleftTurn + "rightleftturn value");

        //}
        rightleftTurn = 0;
    }
    IEnumerator RÝGHTLEFT09scale()
    {
        
        for (int i = 1; i < 91; i++)
        {
            //rightleftTurn++;
            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
            if (i == 15)
            {
                target.transform.localRotation = Quaternion.Euler(-8.96f, 6.44f, 39f);
            }
            rightleftTurn = i;
            yield return new WaitForSeconds(0.000001f);
            if (rightSwipe == true)
            {
                for (int j = rightleftTurn; 0 < j; j--)
                {
                    print("break sonrasý girdimi");
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
                }
                break;

            }
        }
        //if (rightSwipe == true)
        //{
        //    for (int i = rightleftTurn; 0 < i; i--)
        //    {
        //        print("break sonrasý girdimi");
        //        target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * -0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
        //        yield return new WaitForSeconds(0.000001f);
        //    }
        //    print(rightleftTurn + "rightleftturn value");

        //}
        rightleftTurn = 0;
    }
    public int leftrightTurn = 0;
    public int leftToLeft = 0;
    public bool isRightSwipe = false;
    IEnumerator LEFTRÝGHT03scale()
    {
        print("LEFTRÝGHT03scale");
        if (isRightSwipe==true)
        {
            print("LEFTRÝGHT03scale True");
            if (leftToLeft <= 1)
            {
                for (int i = 1; i < 91; i++)
                {
                    target.transform.localPosition = /*new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.18f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z)*/LeftRightScale03List[i-1];
                    if (i == 15)
                    {
                        target.transform.localRotation = Quaternion.Euler(target.transform.localRotation.x, target.transform.localRotation.y, 0);
                    }
                    //display_Text.text = "leftright03scaledeki  deðerleri" + target.transform.localPosition;
                    
                    leftrightTurn = i;
                    yield return new WaitForSeconds(0.005f);
                  /*  if (leftSwipe == true)
                    {
                        for (int j = leftrightTurn; 0 < j; j--)
                        {
                            print("break sonrasý girdimi");
                            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.18f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
                        }
                        print(leftrightTurn + "leftrighturn deðeri");
                        break;

                    }*/
                }
            }

            if (leftToLeft >= 2)
            {

                for (int i = 0; i < 50; i++)
                {
                    target.transform.localPosition = new Vector3(Mathf.Cos((4 * i * Mathf.Deg2Rad)) * -0.1f, target.transform.localPosition.y, target.transform.localPosition.z);
                    yield return new WaitForSeconds(0.005f);
                }
                //yield return new WaitForSeconds(.3f);
                for (int i = 1; i < 91; i++)
                {
                    //rightleftTurn++;
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.18f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.5f, target.transform.localPosition.z);
                    if (i == 15)
                    {
                        target.transform.localRotation = Quaternion.Euler(target.transform.localRotation.x, target.transform.localRotation.y, 0);
                    }
                    //display_Text.text = "leftright03scaledekisoldan saða geçerken  deðerleri" + target.transform.localPosition;
                    yield return new WaitForSeconds(0.005f);
                    if (leftSwipe == true)
                    {
                        print(leftToLeft + "giriyormu");
                        break;

                    }
                }
                print(leftToLeft + "leftToLeft kaç");
                isRightSwipe = false;
            }


        }
        //if (leftSwipe == true)
        //{
        //    for (int i = leftrightTurn; 0 < i; i--)
        //    {
        //        print("break sonrasý girdimi");
        //        target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.18f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.25f, target.transform.localPosition.z);
        //        yield return new WaitForSeconds(0.000001f);
        //    }
        //    print("Leftfalse");


        //}
        leftSwipe = false;
        leftrightTurn = 0;
    }


    IEnumerator LEFTRÝGHT05scale()
    {
        target.transform.localRotation = Quaternion.Euler(target.transform.localRotation.x, target.transform.localRotation.y, 0);
        for (int i = 1; i < 91; i++)
        {


            //print(Mathf.Cos((i * (24 / 100))) + "cos");
            print(Mathf.Sin(i) + "sin");
            print(Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.278f + "sinprint");
            print(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.278f + "cosprint");
            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
            leftrightTurn = i;
            yield return new WaitForSeconds(0.000001f);
            if (leftSwipe == true)
            {
                for (int j = leftrightTurn; 0 < j; j--)
                {
                    print("break sonrasý girdimi");
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
                }
                print(leftrightTurn + "leftrighturn deðeri");
                break;

            }
        }
        //if (leftSwipe == true)
        //{
        //    for (int i = leftrightTurn; 0 < i; i--)
        //    {
        //        print("break sonrasý girdimi");
        //        target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.3f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
        //        yield return new WaitForSeconds(0.000001f);
        //    }
        //    print("Leftfalse");

        //    leftSwipe = false;
        //}
        leftSwipe = false;
        leftrightTurn = 0;
    }
    IEnumerator LEFTRÝGHT09scale()
    {
        target.transform.localRotation = Quaternion.Euler(-0.323f, -0.009f, 0.282f);
        for (int i = 1; i < 91; i++)
        {


            //print(Mathf.Cos((i * (24 / 100))) + "cos");
            print(Mathf.Sin(i) + "sin");
            print(Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.278f + "sinprint");
            print(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.278f + "cosprint");
            target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
            leftrightTurn = i;
            yield return new WaitForSeconds(0.000001f);
            if (leftSwipe == true)
            {
                for (int j = leftrightTurn; 0 < j; j--)
                {
                    print("break sonrasý girdimi");
                    target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
                }
                print(leftrightTurn + "leftrighturn deðeri");
                break;

            }
        }
        //if (leftSwipe == true)
        //{
        //    for (int i = leftrightTurn; 0 < i; i--)
        //    {
        //        print("break sonrasý girdimi");
        //        target.transform.localPosition = new Vector3(Mathf.Cos((2 * i * Mathf.Deg2Rad)) * 0.35f, Mathf.Sin(2 * i * Mathf.Deg2Rad) * 0.7f, target.transform.localPosition.z);
        //        yield return new WaitForSeconds(0.000001f);
        //    }
        //    print("Leftfalse");

        //    leftSwipe = false;
        //}
        leftSwipe = false;
        leftrightTurn = 0;
    }

    //IEnumerator SpeedDown()
    //    {
    //        for (int i = 0; i < 10000; i++)
    //        {
    //            if (isSpeedDown == true)
    //            {
    //                yield return new WaitForSeconds(2f);
    //                print("hýz düþtü");
    //                speed -= 0.5f;
    //                print("2sn geçti");
    //                break;
    //            }
    //        }

    //    }

    void MovePosLEFT()
        {
            DOTween.Kill("firstrightpos");
            //target.transform.DOLocalMove(new Vector3(0.13f, 0.83f, 0.4867598f), 0.5f);
            //yield return new WaitForSeconds(0.5f);
            target.transform.DOLocalMove(new Vector3(-0.022f, 0.623f, 0.48f), 0.5f).SetTarget("firstleftpos");
        }
        IEnumerator MovePosRIGHT()
        {
            DOTween.Kill("firstleftpos");
            //target.transform.DOLocalMove(new Vector3(0.13f, 0.83f, 0.4867598f), 0.5f);
            yield return new WaitForSeconds(0.5f);
            DOTween.Kill("firstleftpos");
            print(DOTween.Kill("firstleftpos"));
            target.transform.DOLocalMove(new Vector3(0.3f, 0.5f, 0.62f), 0.5f).SetTarget("firstrightpos");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "küp")
            {
                speed = Mathf.SmoothStep(speed, speed - 1, 50 * Time.fixedDeltaTime);
            }
            if (other.tag == "Puck")
            {
                scoreValue += 1;
                colorBar.fillAmount += 0.05f;
                scoreBar.fillAmount -= 0.05f;
                Instantiate(starsAnim, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 1), Quaternion.identity);
                puck.transform.localScale = new Vector3(puck.transform.localScale.x + puckTranslate, puck.transform.localScale.y, puck.transform.localScale.z + puckTranslate);
                if (puck.transform.localScale.x >= 0.4f && puck.transform.localScale.x <= 0.6f)
                {
                    puck.transform.localScale = new Vector3(puck.transform.localScale.x, puck.transform.localScale.y + puckTranslate, puck.transform.localScale.z);
                }
                other.transform.gameObject.SetActive(false);
            }
            if (other.tag == "finish")
            {
                isFinish = true;
                playeranim.SetBool("hitFromRightToIdle", true);
                playeranim.SetBool("hitFromLeftToIdle", true);
                transform.DOMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 3), 2f);
            }
            //if (other.tag == "car")
            //{
            //    print("car");
            //    playeranim.SetBool("HitCar", true);
            //    //playeranim.SetBool("HitCar", false);
            //}
        }
        bool IsPointerOverUIObject(Touch touch)
        {
            //UI OBJELERÝNE DEÐMEMESÝ ÝÇÝN BUNUN ÝÇÝN SWÝPE ÝÇÝNE if (!IsPointerOverUIObject(Input.GetTouch(0))) BUNU KOYMALISIN!!
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;

        }


    }

