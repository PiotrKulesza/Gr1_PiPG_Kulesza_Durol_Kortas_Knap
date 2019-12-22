using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE =50f;
    private const float PIPE_WIDTH = 7.8f;
    private const float PIPE_HEAD_HEIGHT = 4f;
    private const float PIPE_MOVE_SPEED = 30f;
    private const float PIPE_DESTROY_X_POSITION = -115f;
    private const float PIPE_SPAWN_X_POSITION = +115f;
    private const float GROUND_DESTROY_X_POSITION = -230f;
    private const float CLOUD_DESTROY_X_POSITION = -160f;
    private const float CLOUD_SPAWN_X_POSITION = +170f;
    private const float CLOUD_SPAWN_Y_POSITION = +27f;
    private const float BIRD_X_POSITION = 0f;

    private static Level instance;

    public static Level GetInstance()
    {
        return instance;
    }

    private List<Transform> groundList;
    private List<Transform> cloudList;
    private float cloudSpawnTimer;
    private List<Pipe> pipeList;
    private int pipesPassedCount;
    private int pipesSpawned;
    private float pipeSpawnTimer;
    private float pipeSpawnTimerMax;
    private float gapSize;
    private State state;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible,
    }

    private enum State 
    {
        WaitingToStart,
        Playing,
        BirdDead,  
    }

    private void Awake()
    {
        instance = this;
        SpawnInitialGround();
        SpawnInitialClouds();
        pipeList = new List<Pipe>();
        pipeSpawnTimerMax = 1f;
        SetDifficulty(Difficulty.Easy);
        state = State.WaitingToStart;
    }
    private void Start()

    {     
        Uni.GetInstance().OnDied += Uni_OnDied;
        Uni.GetInstance().OnStartedPlaying += Uni_OnStartedPlaying;
    }

    private void Uni_OnStartedPlaying(object sender, System.EventArgs e)
    {
        state = State.Playing;
    }

    private void Uni_OnDied(object sender, System.EventArgs e)
    {
        state = State.BirdDead;
    }

    private void Update()
    {
        if(state == State.Playing) { 
        HandlePipeMovement();
        HandlePipeSpawning();
        HadnleGround();
        HandleClouds();
        }
    }

    private void SpawnInitialClouds()
    {
        cloudList = new List<Transform>();
        Transform cloudTransform;
        cloudTransform = Instantiate(GameAssets.GetInstance().pfCloud_1, new Vector3(0, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
        cloudList.Add(cloudTransform);
    }

    private Transform GetCloudPrefabTransform()
    {
        switch (Random.Range(0, 3))
        {
            default:
            case 0: return GameAssets.GetInstance().pfCloud_1;
            case 1: return GameAssets.GetInstance().pfCloud_2;
            case 2: return GameAssets.GetInstance().pfCloud_3;
        }
    }


    private void HandleClouds()
    {
        cloudSpawnTimer -= Time.deltaTime;
        if (cloudSpawnTimer < 0)
        {
            float cloudSpawnTimerMax = 7f;
            cloudSpawnTimer = cloudSpawnTimerMax;       
            Transform cloudTransform = Instantiate(GetCloudPrefabTransform(), new Vector3(CLOUD_SPAWN_X_POSITION, CLOUD_SPAWN_Y_POSITION, 0), Quaternion.identity);
            cloudList.Add(cloudTransform);
        }

        for (int i = 0; i < cloudList.Count; i++)
        {
            Transform cloudTransform = cloudList[i];
            {
                cloudTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime * .7f;

                if (cloudTransform.position.x < CLOUD_DESTROY_X_POSITION)
                {
                    Destroy(cloudTransform.gameObject);
                    cloudList.RemoveAt(i);
                    i++;
                }
            }
        }
    }


    private void SpawnInitialGround()
    {
        groundList = new List<Transform>();
        Transform groundTransform;
        float groundY = -51f;
        float groundWidth = 223f;
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(0, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
        groundTransform = Instantiate(GameAssets.GetInstance().pfGround, new Vector3(groundWidth * 2f, groundY, 0), Quaternion.identity);
        groundList.Add(groundTransform);
    }

    private void HadnleGround()
    {
        foreach (Transform groundTransform in groundList)
        {
            groundTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;

            if (groundTransform.position.x < GROUND_DESTROY_X_POSITION)
            {
                float rightMostXPosition = -115f;
                for(int i = 0; i < groundList.Count; i++)
                {
                    if (groundList[i].position.x > rightMostXPosition)
                    {
                        rightMostXPosition = groundList[i].position.x;
                    }
                }

                float groundWindth = 223f;
                groundTransform.position = new Vector3(rightMostXPosition + groundWindth, groundTransform.position.y, groundTransform.position.z);
            }
        }
    }

    private void HandlePipeSpawning() {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer < 0)
        {
            pipeSpawnTimer += pipeSpawnTimerMax;

            float heightEdgeLimit = 10f;
            float minHeight = gapSize * .5f + heightEdgeLimit;
            float totalHeight = CAMERA_ORTHO_SIZE * 2f;
            float maxHeight = totalHeight - gapSize * .5f - heightEdgeLimit;

            float height = Random.Range(minHeight, maxHeight);
            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
        }
    }

    private  void HandlePipeMovement() {

        for(int i=0;i<pipeList.Count;i++){
            Pipe pipe = pipeList[i];
            bool isToTheRightOfBird = pipe.GetXPosition() > BIRD_X_POSITION;
            pipe.Move();
            if(isToTheRightOfBird && pipe.GetXPosition() <= BIRD_X_POSITION && pipe.IsBottom())
            {
                pipesPassedCount++;
            }
            if(pipe.GetXPosition() < PIPE_DESTROY_X_POSITION)           
            {
                pipe.DestroySelf();
                pipeList.Remove(pipe);
                i--;
            }
        }
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)        {
            case Difficulty.Easy:
                gapSize = 50f;
                pipeSpawnTimerMax = 1.2f;
                break;
            case Difficulty.Medium:
                gapSize = 40f;
                pipeSpawnTimerMax = 1.1f;
                break;
            case Difficulty.Hard:
                gapSize = 33f;
                pipeSpawnTimerMax = 1.0f;
                break;
            case Difficulty.Impossible:
                gapSize = 24f;
                pipeSpawnTimerMax = .8f;
                break;                 
        }
    }
    private Difficulty GetDifficulty()
    {
        if (pipesSpawned >= 30) return Difficulty.Impossible;
        if (pipesSpawned >= 20) return Difficulty.Hard;
        if (pipesSpawned >= 10) return Difficulty.Medium;
        return Difficulty.Easy;
    }
    private void CreateGapPipes(float gapY, float gapSize, float xPosition)
    {
        CreatePipe(gapY - gapSize * .5f,xPosition,true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * .5f, xPosition, false);
        pipesSpawned++;
        SetDifficulty(GetDifficulty());
    }
    private void CreatePipe(float height, float xPosition, bool isBottom)
    {
        //Set up Pipe  Head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        float pipeHeadYPosition;
        if (isBottom) {
            pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * .5f;
        }
        else
        {
            pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + PIPE_HEAD_HEIGHT * .5f;
        }
        pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);
        
        //Set up Pipe  Body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPosition;
        if (isBottom)
        {
            pipeBodyYPosition = -CAMERA_ORTHO_SIZE;

        }
        else
        {
            pipeBodyYPosition = +CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(xPosition, pipeBodyYPosition);
        
        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height );

        BoxCollider2D pipeBodyBoxColider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxColider.size = new Vector2(PIPE_WIDTH, height);
        pipeBodyBoxColider.offset = new Vector2(0f, height * .5f);

        Pipe pipe = new Pipe(pipeHead, pipeBody, isBottom);
        pipeList.Add(pipe);
            }
    public int GetPipesSpawned()
    {
        return pipesSpawned;
    }

    public int GetPipesPassedCount()
    {
        return pipesPassedCount;
    }

    //Represents a single Pipe
    private class Pipe
    {
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;
        private bool isBottom;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform, bool isBottom)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
            this.isBottom = isBottom;
        }
        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
        }
        public float GetXPosition()
        {
            return pipeHeadTransform.position.x;
        }

        public bool IsBottom()
        {
            return isBottom;
        }


        public void DestroySelf()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }
   
}
   