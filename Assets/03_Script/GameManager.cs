using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager m_unique;
    public static GameManager instance { get { return m_unique; } }

    private void Awake()
    {
        if (m_unique == null)
        {
            m_unique = this;
        }
        else
        {
            Debug.LogWarning("게임 매니저 복수 존재");
            Destroy(gameObject);
        }
    }

    [SerializeField] GUIManager m_guiManager;
    public GUIManager guiManager { get { return m_guiManager; } }

    [SerializeField] DataManager m_dataManager;
    public DataManager dataManager { get { return m_dataManager; } }

    [SerializeField] FileManager m_fileManager;
    public FileManager fileManager { get { return m_fileManager; } }

    [SerializeField] ObjectPoolManager m_objectPoolManager;
    public ObjectPoolManager objectPoolManager { get { return m_objectPoolManager; } }

    [SerializeField] Player m_player;
    public Player player { get { return m_player; } }

    [SerializeField] TileControl m_tileManager;
    public TileControl tileManager { get { return m_tileManager; } }

    [SerializeField] UnitManager m_unitManager;
    public UnitManager unitManager { get { return m_unitManager; } }

    [SerializeField] TeamManager m_teamManager;
    public TeamManager teamManager { get { return m_teamManager; } }

    [SerializeField] CouponManager m_couponManager;
    public CouponManager couponManager { get { return m_couponManager; } }

    [SerializeField] RoundManager m_roundManager;
    public RoundManager roundManager { get { return m_roundManager; } }

    [SerializeField] Spawner m_spawner;
    public Spawner spawner { get { return m_spawner; } }

    [SerializeField] GoldMonsterManager m_goldMonsterManaer;
    public GoldMonsterManager goldMonsterManaer { get { return m_goldMonsterManaer; } }

    [SerializeField] MissionManager m_missionManager;
    public MissionManager missionManager { get { return m_missionManager; } }

    [SerializeField] SkillManager m_skillManager;
    public SkillManager skillManager { get { return m_skillManager; } }

    [SerializeField] EffectManager m_effectManager;
    public EffectManager effectManager { get { return m_effectManager; } }

    [SerializeField] SoundManager m_soundManager;
    public SoundManager soundManager { get { return m_soundManager; } }

    public void InitGame()
    {
        //오브젝트 풀 초기화
        //플레이어 초기화
        //미션 초기화
        //라운드 초기화
        //쿠폰 초기화

    }

    public void Btn_Retry()
    {

    }

    public void Btn_Exit()
    {

    }
}