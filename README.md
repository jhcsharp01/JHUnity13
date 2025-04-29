# JHUnity13
 유니티 13기 깃허브 파일

## 목차
  - [2025-04-29](#20250429)


## 20250429
```cs
1. 에셋 스토어에서 아이템으로 쓸만한 에셋 추가(UI)
[에셋](https://assetstore.unity.com/packages/2d/gui/icons/rpg-icons-free-starter-pack-245521)
2. Scripts -> UI -> B_Canvas.cs

3. Canvas에 연결해줍니다.

4. HitText.cs 코드 수정

5. 계층 창에서 Canvas -> B_Canvas로 변경

6. Hit 오브젝트를 프리팹화(Resources 폴더 쪽으로)
  그 후 계층 창에서 Hit 제거

7. Monster.cs 코드 수정
  -> GetDamage 함수 기능 추가
  -> isDead  변수 추가
  -> HitText 풀 생성
  -> 테스트용 Update 쪽에서 GetDamage 호출
      A키 입력

8. Hit 프리팹 클릭
   Hit에 애니메이션 기능 추가

9. Assets 폴더에서 Animations 폴더 생성
   -> UI 폴더 생성
10. Hit 프리팹 선택 후 ctrl + 6 키 
     or Window -> Animation 클릭
    Create 버튼 클릭
   Controller와 Animation Clip 생성
  
컨트롤러 안만들어진 사람은
Create -> Animation -> Animation Controller 눌러서
직접 만들기

그 후 애니메이션 클립을 애니메이터에 드래그


Monster 이펙트 생성 코드를
Pool을 통한 생성으로 변경


11. HitText.cs 텍스트를 위로 서서히 올리는 연출 추가
취향에 따라 적용

12. 특정 시간 뒤에 반납 코드 추가(예정)

13. 코인 UI 만들기
    UI -> Create Empty
    Coin_Move 오브젝트 생성
    UI -> Image
    Gem 이미지 적용
    Coin_Move
     -> Image
     -> Image (1)
     ....

14. Scripts -> UI -> CoinMove.cs
    Coin_Move 오브젝트에 컴포넌트로
    연결

15. Monster.cs 수정을 통해 
    Coin_Move 적용

16. Coin_Move 오브젝트를 프리팹화
    리소스 폴더로 이동

17. 플레이어 관련 코드 구현
Character.cs 수정
 - StrikeFirst 함수 구현
Player.cs 수정

18. Assets -> Animations -> Player 폴더 생성 
  Create -> Animation -> Animation Controller 생성
  "PlayerController"
![image](https://github.com/user-attachments/assets/b1779001-9093-4695-b731-36d82028d387)


19. 플레이어 추적 및 공격 로직(구현 중)

20. 스크립터블 오브젝트 생성

```
