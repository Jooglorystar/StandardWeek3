# StandardWeek3
 
## Q1 분석문제  

입문 주차에서는 자체에서 입력을 판단하고, 그 입력을 움직임을 담당하는 클래스에서 움직임을 구현했었다.  
숙련주차에서는 입력감지와 움직임 구현이 통합되어 있으며, Update에서 Move의 움직임을 실시간으로 구현한다.

CharacterManager는 Player의 정보를 전역적으로 사용할 수 있게 해준다.  

키가 눌렸을 때, OnMoveinput에서 입력받은 값을 moveInput에 반영하고, 입력이 종료되면 Vector2.zero로 초기화된다. Move에서 이 입력받은 값의 방향을 Vector3로 적절하게 받고, 캐릭터의 이동속도만큼 빠르게 하며, y축에 대한 velocity는 플레이어 rigidbody에 맞춘다.

기본적으로 마우스의 Delta값을 받으며, 민감도 조절을 한다. 마우스를 위 아래로 움직일 때, Mathf.Clamp를 이용해 최대값과 최소값을 정해, 무한히 회전하는 것을 막았다. 그리고 카메라를 포함하고 있는 CameraContainer오브젝트의 rotation x값을 조작했다.  
다신 좌우로 이동하는 것은 플레이어 오브젝트 자체를 조작한다.  
단순히 space바를 조작했을 때 점프를 가능하게 하면 무한히 점프할 수 있는 문제가 발생한다. 따라서 bool값을 반환하는 IsGround()메서드에서 Ray를 아래로 짧게 발사하여, 무언가 접촉하면 바닥에 있음을 의미하는 true를 반환하고, 그렇지 않으면(공중에 있으면) false를 반환한다.

FixedUpdate는 고정된 프레임으로 실행되는 메서드로 알고 있고, LateUpdate는 Update류 메서드들 중에 제일 마지막에 실행하는 메서드로 알고 있다.  
움직임을 FixedUpdate에서 구현하는 까닭은 매 프레임마다 update를 할 경우 실행 환경에 따라 다른 결과가 나올 확률이 있지만, 고정된 프레임 값을 이용할 경우 그 문제를 줄일 수 있기 때문이다. LateUpdate에 카메라 움직임을 넣은 이유는, 모든 움직임이 끝난 후에 카메라를 움직여, 혹시라도 있을 표시 오류를 방지하기 위함이다.

## Q2 분석문제

별도의 UI스크립트를 만드는 이유는 추가적인 UI가 필요할 때 추가가 용이하기 때문이다.  
본 예시에서는 TakePhysicalDamage()라는 메서드가 포함된 IDamagable이라는 인터페이스를 사용했다. 그리고 CampFire에서 해당 인터페이스를 가진 모든 오브젝트에 데미지를 줄 수 있는 식의 구현을 하였다.  
이를 통해 플레이어가 아니더라도, IDamagable이 상속된 적 유닛도 CampFire에 접근한다면 데미지를 입을 수 있다.  
UI는 각 수치를 Condition.cs를 통해 증감과 fillAmount관리를 하고, 이를 UIconditions.cs에서 통합해 관리한다.  
이는 CharacterManager에서 접근할 수 있도록 Player오브젝트에도 추가한다.

## Q3 분석문제

기본적으로 바닥에 떨어진 오브젝트들은 IInteractable라는 인터페이스를 상속받는다.  
플레이어는 화면 가운데에서 아이템을 감지하는 Ray를 발사해 감지하고, 해당하는 아이템이 있다면 그 아이템에서 IInteractable을 받아오고, 그 정보를 띄워주는 역할을 한다.  
IInteractable은 GetInteractPrompt()와 OnInteract()라는 메서드를 가지고 있는데, ItemObject에서는 GetInteractPrompt()에서 아이템의 이름과 설명 데이터를 string 값으로 반환하고, OnInteract()는 아이템 획득에 관련된 메서드가 구현되어있다.  
인벤토리는 크게 아이템슬롯들과 선택한 아이템의 정보를 표시하는 정보창이 있다.  
