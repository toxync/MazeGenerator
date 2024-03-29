# MazeGenerator
C#으로 제작된 랜덤 미로 생성 & 미로 경로 탐색 프로그램입니다.

## 핵심 기능 요약

* **[Wilson's Algorithm](#wilsons-algorithm)을 사용한 랜덤 미로 생성**
* **[A* 알고리즘](#a-알고리즘)을 사용한 미로 경로 탐색**
* **파일 다이얼로그를 사용한 생성한 미로 데이터 저장 & 불러오기**
* **출발점부터 도착점까지의 미로 경로 이동을 표시해주는 애니메이션**

## 기능 설명

### 랜덤 미로 생성 & 초기화

* '미로 생성' 버튼을 클릭해서 랜덤 미로를 생성할 수 있습니다.
* 랜덤 미로 생성에 [Wilson's Algorithm](#wilsons-algorithm)이 사용됩니다.
* '미로 초기화' 버튼을 클릭해서 생성된 랜덤 미로를 삭제할 수 있습니다.

<details>
  <summary>랜덤 미로 생성 & 초기화(펼치기/접기)</summary><br>

  ![GenerateRandomMazeDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/generate_random_maze_demo.gif)
</details>

### 미로 데이터 저장

* '미로 데이터 저장' 버튼을 클릭해서 생성된 랜덤 미로 데이터를 텍스트 파일로 저장할 수 있습니다.

<details>
  <summary>미로 데이터 저장(펼치기/접기)</summary><br>

  ![SaveMazeDataDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/save_maze_data_demo.gif)
</details>

### 미로 데이터 읽기 & 불러오기

* '목록에 미로 추가' 버튼을 클릭해서 미로 데이터를 읽어들일 수 있습니다.
* 읽어들인 미로 목록에서 미로 데이터를 선택한 다음 '미로 데이터 불러오기' 버튼을 클릭하여 읽어들인 미로 데이터를 불러올 수 있습니다.

<details>
  <summary>미로 데이터 읽기 & 불러오기(펼치기/접기)</summary><br>

  ![LoadMazeDataDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/load_maze_data_demo.gif)
</details>

### 미로 경로 탐색

* 미로 영역 안에서 마우스 좌클릭으로 출발점, 마우스 우클릭으로 도착점을 설정할 수 있습니다.
* 출발점과 도착점이 설정됐으면 '경로 탐색' 버튼을 클릭해서 경로를 탐색할 수 있습니다.
* 경로 탐색에 [A* 알고리즘](#a-알고리즘)이 사용됩니다.
* 경로 탐색에 성공했다면 경로를 빨간색 실선으로 보여줍니다.
* 경로 탐색에 실패했다면 경로가 없음을 알리는 메시지 박스를 생성합니다.

<details>
  <summary>경로 탐색 성공(펼치기/접기)</summary><br>

  ![FindMazePathDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/find_maze_path_demo.gif)
</details>

<details>
  <summary>경로 탐색 실패(펼치기/접기)</summary><br>

  ![NoMazePathDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/no_maze_path_demo.gif)
</details>

### 경로 애니메이션 재생

* 설정된 출발점과 도착점 사이에 경로가 존재한다면 '경로 애니메이션 재생' 버튼을 클릭해서 출발점부터 도착점까지의 경로 애니메이션을 볼 수 있습니다.

<details>
  <summary>경로 애니메이션 재생(펼치기/접기)</summary><br>

  ![ShowPathMotionDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/show_path_motion_demo.gif)
</details>

### 읽어들인 미로 데이터 삭제

* 읽어들인 미로 데이터 목록에서 미로 데이터 항목을 선택한 다음 '목록에서 미로 삭제' 버튼을 클릭하여 읽어들인 미로 데이터를 삭제할 수 있습니다.
* 마우스 좌클릭 드래그로 한 번에 여러 개의 미로 데이터 항목을 선택해서 삭제할 수 있습니다.

<details>
  <summary>미로 데이터 삭제(펼치기/접기)</summary><br>

  ![RemoveMazeDataDemo](https://raw.githubusercontent.com/toxync/MazeGenerator/master/Maze_Generator/demo_images/remove_maze_data_demo.gif)
</details>

### Wilson's Algorithm

* 미로 안에서의 무작위 이동을 통해 미로 모양을 결정하는 알고리즘입니다.

#### Wilson's Algorithm의 동작 과정

1. 모든 미로 칸들을 '상하좌우에 벽이 있고 미로에 포함되지 않은 칸'으로 초기화함

2. 무작위로 선택한 미로 칸을 '미로에 포함된 칸'으로 변경시킴

3. '미로에 포함되지 않은 칸'들 중 하나를 출발점로 설정함

4. '미로에 포함된 칸'에 도착할 때까지 출발점부터 상하좌우로 무작위 이동을 하며 무작위 이동을 할 때마다 이동방향을 기록함

5. '미로에 포함된 칸에' 도착했다면 출발점부터 도착점까지 기록된 이동방향을 따라 이동하면서 이동 경로에 있는 모든 미로 칸들을 '미로에 포함된 칸'으로 변경시키고 이동방향에 해당하는 벽을 제거함
([5][6] 칸에서 [5][5] 칸으로 윗방향 이동을 했다면 [5][6] 칸의 위쪽 벽과 [5][5] 칸의 아래쪽 벽을 제거함)

6. 모든 미로 칸들이 '미로에 포함된 칸'이 될 때까지 3 ~ 5번 과정을 반복함

### A* 알고리즘

* 이동 비용을 계산해서 출발점과 도착점 사이의 최단 거리 경로를 구해내는 알고리즘입니다.

#### A* 알고리즘의 동작 과정

1. '시작점에서 이동가능한 미로 칸'들을 '열린 칸' 목록에 추가하고 시작점을 '닫힌 칸' 목록에 추가함

2. 시작점을 '열린 칸'들의 부모로 설정함

3. '열린 칸' 목록에 있는 칸들 중에서 F 비용이 가장 작은 칸을 선택함

> F = G + H
>
> G: 시작점부터 현재 칸까지의 경로를 따라 이동하는 데에 필요한 비용
>
> H: 현재 칸부터 도착점까지의 이동 예상 비용

4. 선택한 칸을 '열린 칸' 목록에서 삭제하고 '닫힌 칸' 목록에 추가함

5. '선택한 칸에서 이동가능한 미로 칸'들을 '열린 칸' 목록에 추가하고 해당 칸들의 부모를 현재 선택한 칸으로 설정함

> '선택한 칸에서 이동가능한 미로 칸'이 '열린 칸' 목록에 이미 존재한다면 해당 칸의 부모를 변경하지 않음

6. '선택한 칸에서 이동가능한 미로 칸'이 '열린 칸' 목록에 이미 존재한다면 해당 칸들의 G 비용을 재평가함

> '선택한 칸에서 이동가능한 미로 칸'의 기존 부모 기준 G 비용보다 '선택한 칸에서 이동가능한 미로 칸'의 부모가 선택한 칸일 경우의 G 비용이 더 낮다면 선택한 칸을 '선택한 칸에서 이동가능한 미로 칸'의 부모로 변경함

7. '선택한 칸에서 이동가능한 미로 칸'의 부모가 변경됐다면 해당 칸의 G와 F 비용을 다시 계산함

8. 도착점이 '열린 칸' 목록에 추가됐거나(경로 완성됨) '열린 칸' 목록이 비어버릴 때까지(경로 없음) 3 ~ 7번 과정을 반복함

## 참고 자료

* Wilson's Algorithm
  - https://weblog.jamisbuck.org/2011/1/20/maze-generation-wilson-s-algorithm
* A* 알고리즘
  - https://itmining.tistory.com/66
