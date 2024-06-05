# 🎮 내일배움캠프 6주차 유니티 숙련 개인과제 <img src="https://img.shields.io/badge/Unity-FFFFFF?style=flat&logo=Unity&logoColor=5D5D5D"/> <img src="https://img.shields.io/badge/C%23-5D5D5D?style=flat&logo=csharp&logoColor=FFFFFF"/>       
 
<div align="center">
  <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSGGtUQnh2auP6-5piyLjjhl-X2ScndYQy1bBnparJO&s"></img>


## 6주차 개인 프로젝트
</div>
  
### 🌒 프로젝트 정보
`개요` : 3D Game    

`기술` : Unity, C#    

`팀원` : 이시원

---
### 🌔 구현 목록

##### 1. 캐릭터 이동 및 점프   
<details><summary>사진,코드 펼치기</summary>
![test1](https://github.com/SnowScapes/LowPolyAdventure/assets/39547945/7c066e47-f0da-483d-ae3e-a233abde9bf8)    

```csharp
    private void Awake()
    {
        stat = GetComponent<PlayerStatHandler>().stat;
    }

    private void Start()
    {
        moveSpeed = stat.Speed;
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void LateUpdate()
    {
        OnLook();
    }

    private void OnMove()
    {
        velocity = transform.forward * keyboardInput.y + transform.right * keyboardInput.x;
        velocity *= moveSpeed;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void OnLook()
    {
        transform.eulerAngles += new Vector3(0, mouseInput.x, 0);
        CharaCamera.y_input = -mouseInput.y;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        keyboardInput = context.ReadValue<Vector2>().normalized;
    }

    public void LookInput(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
        mouseInput *= mouseSpeed;
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            _rigidbody.AddForce(Vector2.up * stat.Jump, ForceMode.Impulse);
    }

    public void InventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            UIManager.instance.ToggleInventory();
    }
```
</details>

##### 2. 3인칭 카메라   
<details><summary>사진,코드 펼치기</summary>
![test2](https://github.com/SnowScapes/LowPolyAdventure/assets/39547945/9ce3207b-f623-4656-a850-a315cf08ac67)    

```csharp
public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance = 5;
    [SerializeField] private float minRot;
    [SerializeField] private float maxRot;

    private float curRot;

    public float y_input { get; set; }

    private void LateUpdate()
    {
        SetPos();
    }

    private void SetPos()
    {
        curRot = transform.eulerAngles.x;
        curRot += y_input;
        if (curRot < 180) 
            curRot = Mathf.Clamp(curRot, -1, maxRot);
        else
            curRot = Mathf.Clamp(curRot, 360 + minRot, 361);
        transform.eulerAngles = new Vector3(curRot, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
```

</details>

##### 3. 아이템 구현   
<details><summary>사진,코드 펼치기</summary>

```csharp
// 아이템 정보 저장용 SO
[CreateAssetMenu (menuName = "ItemInfo", fileName = "Default", order = 0)]
public class ItemSO : ScriptableObject
{
    public Define.ItemType ItemType;
    public string ItemName;
    public int Value;
    public string Description;
}

```

```csharp
// 부모 클래스 형식
public class Item : MonoBehaviour
{
    public ItemSO ItemInfo;
    [SerializeField] protected PlayerCondition pCondition;
    
    protected float value;
}
```
```csharp
// 자식 클래스 형식
public class HungerItem : Item, IUseable
{
    public void Use()
    {
        pCondition.conditionArgs.changeHungerValue = value;
        pCondition.CallHungerEvent();
        pCondition.conditionArgs.changeHungerValue = -0.05f;
    }
}
```

```csharp
// 전략패턴 사용해보기
// 아이템 사용코드
    private IUseable curItem;
    private List<IUseable> items = new List<IUseable>();

    private void SelectItem(int index)
    {
        curItem = items[index];
    }
    
    private void Activate()
    {
        curItem.Use();
    }
```


</details>

##### 4. 플레이어 상태 UI   
<details><summary>사진,코드 펼치기</summary>
![image](https://github.com/SnowScapes/LowPolyAdventure/assets/39547945/3a064079-c305-4bf5-ba8f-66c8fef63bde)    

```csharp
// 플레이어 오브젝트에 붙일 상태 관리 코드
public class PlayerCondition : MonoBehaviour
{
    public event Action<ConditonArgs> HealthEvent;
    public event Action<ConditonArgs> HungerEvent;
    public event Action<ConditonArgs> StaminaEvent; 
    
    public ConditonArgs conditionArgs;
    
    [SerializeField] public PlayerStatHandler stathandler;

    private void Start()
    {
        conditionArgs = new ConditonArgs(stathandler.stat.Hunger, stathandler.currentHunger);
        HungerEvent += reduceHunger;
        StartCoroutine(reduceHungerCouroutine());
    }

    public void CallHungerEvent()
    {
        HungerEvent?.Invoke(conditionArgs);
    }

    public void CallHealthEvent()
    {
        HealthEvent?.Invoke(conditionArgs);
    }

    public void callStaminaEvent()
    {
        StaminaEvent?.Invoke(conditionArgs);
    }

    private void reduceHunger(ConditonArgs args)
    {
        stathandler.currentHunger = stathandler.currentHunger < 0 ? 0 : stathandler.currentHunger + args.changeHungerValue;
        args.curHunger = stathandler.currentHunger;
    }

    private IEnumerator reduceHungerCouroutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while (true)
        {
            CallHungerEvent();
            yield return delay;
        }
    }
}
```
```csharp
// UI 관리 코드
public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Image conditionUiImage;

    public void SetFill(ConditonArgs args)
    {
        conditionUiImage.fillAmount = args.curHunger / args.maxHunger;
    }
}
```


</details>
