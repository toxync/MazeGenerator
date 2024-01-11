# MazeGenerator
C#으로 제작된 랜덤 미로 생성 & 미로 경로 탐색 프로그램입니다.

## 핵심 기능 요약

* **[Wilson's Algorithm](wilsons-algorithm)을 사용한 랜덤 미로 생성**
* **[A* 알고리즘](a-알고리즘)을 사용한 미로 경로 탐색**
* **파일 다이얼로그를 사용한 생성한 미로 데이터 저장 & 불러오기**
* **출발점부터 도착점까지의 미로 경로 이동을 표시해주는 애니메이션**

## 기능 설명

### 랜덤 미로 생성 & 초기화

* '미로 생성' 버튼을 클릭해서 랜덤 미로를 생성할 수 있습니다.
* 랜덤 미로 생성에 [Wilson's Algorithm](wilsons-algorithm)이 사용됩니다.
* '미로 초기화' 버튼을 클릭해서 생성된 랜덤 미로를 삭제할 수 있습니다.

<details>
  <summary>랜덤 미로 생성 & 초기화 펼치기/접기</summary><br>

  
</details>

### 미로 데이터 저장

* '미로 데이터 저장' 버튼을 클릭해서 생성된 랜덤 미로 데이터를 텍스트 파일로 저장할 수 있습니다.

<details>
  <summary>미로 데이터 저장 펼치기/접기</summary><br>

  
</details>

### 미로 데이터 읽기 & 불러오기

* '목록에 미로 추가' 버튼을 클릭해서 미로 데이터를 읽어들일 수 있습니다.
* 읽어들인 미로 목록에서 미로 데이터를 선택한 다음 '미로 데이터 불러오기' 버튼을 클릭하여 읽어들인 미로 데이터를 불러올 수 있습니다.

<details>
  <summary>미로 데이터 읽기 & 불러오기 펼치기/접기</summary><br>

  
</details>

### 미로 경로 탐색

* 미로 영역 안에서 마우스 좌클릭으로 출발점, 마우스 우클릭을 도착점을 설정할 수 있습니다.
* 출발점과 도착점이 설정됐으면 '경로 탐색' 버튼을 클릭해서 경로를 탐색할 수 있습니다.
* 경로 탐색에 [A* 알고리즘](a-알고리즘)이 사용됩니다.
* 경로 탐색에 성공했다면 경로를 빨간색 실선으로 보여줍니다.
* 경로 탐색에 실패했다면 경로가 없음을 알리는 메시지 박스를 생성합니다.

<details>
  <summary>경로 탐색 성공 펼치기/접기</summary><br>

  
</details>

<details>
  <summary>경로 탐색 실패 펼치기/접기</summary><br>

  
</details>

### 경로 애니메이션 재생

* 설정된 출발점과 도착점 사이의 경로가 존재한다면 '경로 애니메이션 재생' 버튼을 클릭해서 출발점부터 도착점까지의 경로 애니메이션을 볼 수 있습니다.

<details>
  <summary>경로 애니메이션 재생 펼치기/접기</summary><br>

  
</details>

### 읽어들인 미로 데이터 삭제

* 읽어들인 미로 데이터 목록에서 미로 데이터 항목을 선택한 다음 '목록에서 미로 삭제' 버튼을 클릭하여 읽어들인 미로 데이터를 삭제할 수 있습니다.
* 마우스 좌클릭 드래그로 한 번에 여러 개의 미로 데이터 항목을 선택해서 삭제할 수 있습니다.

<details>
  <summary>미로 데이터 삭제 펼치기/접기</summary><br>

  
</details>

### Wilson's Algorithm



### A* 알고리즘



## 참고 자료

* Wilson's Algorithm
  - https://weblog.jamisbuck.org/2011/1/20/maze-generation-wilson-s-algorithm
* A* 알고리즘
  - https://itmining.tistory.com/66
