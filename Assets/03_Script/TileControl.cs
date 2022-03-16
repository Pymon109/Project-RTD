using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour
{
    static TileControl _unique;
    public static TileControl _instance { get { return _unique; } }

    public MouseControl m_mouseCon; 
    public List<Tile> _list_tiles;
    Tile _target;
    public bool isTargetSet { get { return _target != null; } }
    public int IdxOfTarget() { return int.Parse(_target.name); }
    Tile _sameUnitTile;
    bool _isCouponMod = false;
    Tile _couponTarget;

    [SerializeField] GUI_CreateUnitButton m_guiCreateUnitButton;
    public bool couponUnitCreate(int sid)
    {
        if(_couponTarget)
        {
            _couponTarget.CreateUnit(sid);
            return true;
        }
        return false;
    }

    public void SwitchCouponMode(bool command)
    {
        switch(command)
        {
            case true:
                _target = null;
                _sameUnitTile = null;
                //설치 가능 타일 검색
                SearchInstallableTiles();
                break;
            case false:
                if (_couponTarget)
                    _couponTarget.SetTileState(Tile.E_TileState.NONE);
                _couponTarget = null;
                CouponManager._instance.SwitchCouponUnitCard(false, Vector3.zero);
                break;
        }
        _isCouponMod = command;
    }

    public void SetTargetTile(RaycastHit hit)
    {
        if (_isCouponMod)
        {
            Tile temp = hit.collider.gameObject.GetComponent<Tile>();
            if (temp.GetTileState() == Tile.E_TileState.COUPON_WAIT)
            {
                _couponTarget = temp;
                _couponTarget.SetTileState(Tile.E_TileState.COUPON_SELECT);
                //해당 자리에 유닛 선택 카드 열기
                Vector3 pos = hit.transform.position;
                pos.y = 50;
                CouponManager._instance.SwitchCouponUnitCard(true, pos);
                //유닛 카드 데이터 뿌리기

                //다른 유닛 이펙트 끄기
                ReleaseCouponWaitTiles();
            }
        }
        else
        {
            if (_target)
            {
                //선택 이펙트 꺼주기
                EffectSwitcher(false);
            }
            _target = hit.collider.gameObject.GetComponent<Tile>();
            //같은 유닛 있는지 검색
            FindSameUnit(_target.GetUnit());
            EffectSwitcher(true);
            _target.PlaySelectAudio();
            m_guiCreateUnitButton.ButtonSetActive(true);
        }
    }
    public void ReleaseTargetTile()
    {
        //선택 이펙트 꺼주기
        EffectSwitcher(false);
        ReleaseCouponWaitTiles();
        SwitchCouponMode(false);
        m_guiCreateUnitButton.ButtonSetActive(false);
    }

    public bool FindSameUnit(Unit targetUnit)
    {
        if (targetUnit)
        {
            if (targetUnit.IsLocked())
            {
                _sameUnitTile = null;
                return false;
            }
            if(targetUnit.GetRank() == UnitManager.E_Rank.EPIC)
            {
                _sameUnitTile = null;
                return false;
            }
            int sameUnitIDX = UnitManager._instance.FindSameUnit(targetUnit.GetSID(), int.Parse(_target.gameObject.name));
            if (sameUnitIDX >= 0)
            {
                _sameUnitTile = _list_tiles[sameUnitIDX];
                _target.SetTileState(Tile.E_TileState.MERGEABLE);
                _sameUnitTile.SetTileState(Tile.E_TileState.MERGEABLE);
                return true;
            }
            else
                _target.SetTileState(Tile.E_TileState.MERGE_NOT_ALLOWED);
        }
        return false;
    }

    public void EffectSwitcher(bool on)
    {
        switch(on)
        {
            case true:
                if(_target)
                    _target.SelectTile(true);
                if (_sameUnitTile)
                    _sameUnitTile.ActiveEffect_select(true);
                break;
            case false:
                if (_target)
                {
                    _target.SelectTile(false);
                    _target = null;
                }
                if (_sameUnitTile)
                {
                    _sameUnitTile.ActiveEffect_select(false);
                    _sameUnitTile = null;
                }
                break;
        }
    }

    public void MergeUnit()
    {
        _target.CreateUnit();
        _sameUnitTile.DeleteUnit();
        _sameUnitTile.ActiveEffect_select(false);
        _sameUnitTile = null;
    }

    void SearchInstallableTiles()
    {
        for(int i = 0; i < _list_tiles.Count; i++)
        {
            if (_list_tiles[i].GetTileState() == Tile.E_TileState.NONE)
                _list_tiles[i].SetTileState(Tile.E_TileState.COUPON_WAIT);
        }
    }

    void ReleaseCouponWaitTiles()
    {
        for (int i = 0; i < _list_tiles.Count; i++)
        {
            if (_list_tiles[i].GetTileState() == Tile.E_TileState.COUPON_WAIT)
                _list_tiles[i].SetTileState(Tile.E_TileState.NONE);
        }
    }

    public void ButtonOnUnitSell()
    {
        if (_target)
            _target.SellUnit();
    }

    public void ButtonOnUnitLock()
    {
        if (_target)
            _target.LockUnit();
    }

    public void ButtonOnCreateUnit()
    {
        if(_target.GetTileState() == Tile.E_TileState.MERGEABLE)
        {
            MergeUnit();
            _target.SetMergeble();
        }
        else if (_target.GetTileState() == Tile.E_TileState.NONE)
        {
            if (Player._instance.SpendGold(100))
            {
                _target.CreateUnit();
                _target.SetMergeble();
            }
        }
    }

    private void Awake()
    {
        _unique = this;
        for (int i = 0; i < transform.childCount; i++)
            _list_tiles.Add(transform.GetChild(i).gameObject.GetComponent<Tile>());
    }
}
