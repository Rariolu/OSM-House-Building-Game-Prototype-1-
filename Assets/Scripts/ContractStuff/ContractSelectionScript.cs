using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContractSelectionScript : MultitonScript<ContractSelectionScript,int>
{
    public Text lblContractName;
    public UIButton btnStart;
    public UIButton btnBrochure;
    public UIButton btnBack;
    public UIButton btnForward;
    public Image pbClosedContract;
    public Image pbOpenContract;
    public Image pbOpenMeta;
    public Image[] pbFloorPlans;
    public Text lblInfo;
    
    Contract contract;
    int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

    Image image;
    Image Image
    {
        get
        {
            return image ?? (image = GetComponent<Image>());
        }
    }

    static int count = 0;
    enum ContractSelectionState
    {
        CLOSED,
        OPEN_MAIN,
        OPEN_META
    }
    ContractSelectionState state = ContractSelectionState.CLOSED;
    ContractSelectionState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;

            if (state != ContractSelectionState.CLOSED)
            {
                OpenContract();
            }

            if (pbOpenContract != null)
            {
                pbOpenContract.gameObject.SetActive(state == ContractSelectionState.OPEN_MAIN);
            }

            if (pbOpenMeta != null)
            {
                pbOpenMeta.gameObject.SetActive(state == ContractSelectionState.OPEN_META);
            }

            if (pbClosedContract != null)
            {
                pbClosedContract.gameObject.SetActive(state == ContractSelectionState.CLOSED);
            }

            if (btnForward != null)
            {
                btnForward.gameObject.SetActive(state != ContractSelectionState.CLOSED);
            }

            if (btnBack != null)
            {
                btnBack.gameObject.SetActive(state != ContractSelectionState.CLOSED);
            }

            if (Image != null)
            {
                Image.enabled = state == ContractSelectionState.CLOSED;
            }
        }
    }
    private void Awake()
    {
        SetInstance(id = count++, this);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    public void SetContract(Contract _contract)
    {
        contract = _contract;

        if (lblContractName != null)
        {
            lblContractName.text = contract.name;
        }

        FINISHED_CONSTRUCTION conType = contract.finishedConstruction;

        for (int i = 0; i < pbFloorPlans.Length; i++)
        {
            SetFloorPlan(conType, i, pbFloorPlans[i]);
        }

        if (lblInfo != null)
        {
            StringBuilderPro stringBuilder = new StringBuilderPro();

            stringBuilder.AppendLineFormat("Budget: £{0}", contract.budget);
            stringBuilder.AppendLineFormat("Time: {0}", contract.time);

            lblInfo.text = stringBuilder.ToString();
        }
    }

    void SetFloorPlan(FINISHED_CONSTRUCTION conType,int floor,Image pbFloorPlan)
    {
        string strFloor = "{0}_floorplan_{1}".FormatText(conType, floor);
        Sprite sprite;
        if (ResourceManager.GetItem(strFloor,out sprite))
        {
            pbFloorPlan.sprite = sprite;
        }
        else
        {
            pbFloorPlan.gameObject.SetActive(false);
            Logger.Log("{0} not found in resource manager.",LogType.Warning,strFloor);
        }
    }
    
    void Start()
    {
        State = ContractSelectionState.CLOSED;
        if (btnStart != null)
        {
            btnStart.Click += StartContract;
        }
        else
        {
            Logger.Log("btnStart was null.");
        }
        if (btnBrochure != null)
        {
            btnBrochure.Click += btnBrochure_Click;
            //btnBrochure.Click += (sender) => { Logger.Log("btnBrochure clicked."); };
        }
        else
        {
            Logger.Log("btnBrochure was null.");
        }

        if (btnForward != null)
        {
            btnForward.Click += btnForward_Click;
        }

        if (btnBack != null)
        {
            btnBack.Click += btnBack_Click;
        }
    }

    void btnBrochure_Click(UIButton sender)
    {
        State = ContractSelectionState.OPEN_MAIN;
    }

    void btnForward_Click(UIButton sender)
    {
        State = State != ContractSelectionState.OPEN_META ? (ContractSelectionState)((int)State + 1) : ContractSelectionState.CLOSED;
    }

    void btnBack_Click(UIButton sender)
    {
        State = State != ContractSelectionState.CLOSED ? (ContractSelectionState)((int)State - 1) : ContractSelectionState.OPEN_META;
    }

    void StartContract(UIButton sender)
    {
        if (contract != null)
        {
            Clear();
            Util.SetContract(contract);
        }
        else
        {
            Logger.Log("Contract was null.");
        }
    }

    void OpenContract()
    {
        //Close the other contracts
        foreach(ContractSelectionScript contractSelection in Values)
        {
            if (contractSelection.ID != ID)
            {
                contractSelection.CloseContract();
            }
        }

    }

    public void CloseContract()
    {
        State = ContractSelectionState.CLOSED;
    }
}