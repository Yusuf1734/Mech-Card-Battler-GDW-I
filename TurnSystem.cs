using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnSystem : MonoBehaviour
{
    // set up the turn phases for the game
    public enum TurnPhase
    {
        P1PlanMovement,
        P1PlanAttack,
        ShowPassScreen1,
        P2PlanMovement,
        P2PlanAttack,
        ShowPassScreen3,
        ResolveAttacks,
        ShowPassScreen2
    }

    // set up the inital setup
    public enum InitialSetupPhase
    {
        P1Equip,
        AShowPassScreen1,
        P2Equip,
        AShowPassScreen2
    }

    // variables
    [SerializeField] private TurnPhase currentPhase = TurnPhase.P1Equip;
    [SerializeField] private int currentRound = 1;
    [SerializeField] private UnityEvent onP1Equip = new UnityEvent();
    [SerializeField] private UnityEvent onP2Equip = new UnityEvent();
    [SerializeField] private UnityEvent onP1PlanAttack = new UnityEvent();
    [SerializeField] private UnityEvent onP2PlanAttack = new UnityEvent();
    [SerializeField] private UnityEvent onResolveAttacks = new UnityEvent();
    [SerializeField] private UnityEvent<int> onRoundChanged = new UnityEvent<int>();
    [SerializeField] private UnityEvent<TurnPhase> onPhaseChanged = new UnityEvent<TurnPhase>();
    [SerializeField] private GameObject passScreen1;
    [SerializeField] private GameObject passScreen2;
    [SerializeField] private GameObject passScreen3;
    [SerializeField] private PlayerMovement player1;
    [SerializeField] private PlayerMovement player2;

    // bools
    private bool p1MovementSubmitted;
    private bool p2MovementSubmitted;
    private bool attacksResolved;

    // getters
    public TurnPhase CurrentPhase => currentPhase;
    public int CurrentRound => currentRound;

    private void Awake()
    {
        HidePassScreen(); // ensure that the pass screens are inactive at the start of the game
    }

    private void Start()
    {
        EnterPhase(InitialSetupPhase.P1Equip);
    }

    // method to confirm the first player's equipment phase and move to the next phase
    public void ConfirmP1Equip()
    {
        if (currentPhase != TurnPhase.P1Equip)
            return;

        EnterPhase(TurnPhase.P2Equip);
    }

    // method to confirm the second player's equipment phase and move to the next phase
    public void ConfirmP2Equip()
    {
        if (currentPhase != TurnPhase.P2Equip)
            return;

        EnterPhase(TurnPhase.P1PlanAttack);
    }

    // method to confirm the first player's attack to the next phase
    public void ConfirmP1Attack()
    {
        if (currentPhase != TurnPhase.P1PlanAttack)
            return;

        EnterPhase(TurnPhase.P2PlanAttack);
    }

    // method to confirm the second player's attack to the next phase
    public void ConfirmP2Attack()
    {
        if (currentPhase != TurnPhase.P2PlanAttack)
            return;

        EnterPhase(TurnPhase.ResolveAttacks);
    }

    // method to confirm the first player's movement and move to the next phase
    public void ConfirmP1Movement()
    {
        if (currentPhase != TurnPhase.P1PlanMovement || player1Movement == null)
            return;

        p1MovementSubmitted = true;
        EnterPhase(TurnPhase.P1PlanAttack);
    }

    // method to confirm the second player's movement and move to the next phase
    public void ConfirmP2Movement()
    {
        if (currentPhase != TurnPhase.P2PlanMovement || player2Movement == null)
            return;

        p2MovementSubmitted = true;
        EnterPhase(TurnPhase.P2PlanAttack);
    }

    // method to complete the attack resolution and move to the next round
    public void CompleteAttackResolution()
    {
        if (currentPhase != TurnPhase.ResolveAttacks)
            return;

        currentRound++;
        onRoundChanged.Invoke(currentRound);
        EnterPhase(TurnPhase.P1Equip);
    }

    // method to enter a new phase and invoke the appropriate events
    private void EnterPhase(TurnPhase phase)
    {
        currentPhase = phase;
        onPhaseChanged.Invoke(currentPhase);

        switch (currentPhase)
        {
            case TurnPhase.P1Equip:
                onP1Equip.Invoke();
                break;

            case TurnPhase.P2Equip:
                onP2Equip.Invoke();
                break;

            case TurnPhase.P1PlanAttack:
                onP1PlanAttack.Invoke();
                break;

            case TurnPhase.P2PlanAttack:
                onP2PlanAttack.Invoke();
                break;

            case TurnPhase.ResolveAttacks:
                onResolveAttacks.Invoke();
                break;
        }
    }

    private void ShowPassScreen1()
    {
        passScreen1.SetActive(true);
        passScreen2.SetActive(false);
        passScreen3.SetActive(false);
    }

    private void ShowPassScreen2()
    {
        passScreen1.SetActive(false);
        passScreen2.SetActive(true);
        passScreen3.SetActive(false);
    }

    private void ShowPassScreen3()
    {
        passScreen1.SetActive(false);
        passScreen2.SetActive(false);
        passScreen3.SetActive(true);
    }

    private void AShowPassScreen1()
    {
        passScreen1.SetActive(true);
        passScreen2.SetActive(false);
        passScreen3.SetActive(false);
    }

    private void AShowPassScreen2()
    {
        passScreen1.SetActive(false);
        passScreen2.SetActive(true);
        passScreen3.SetActive(false);
    }

    private void HidePassScreen()
    {
        passScreen1.SetActive(false);
        passScreen2.SetActive(false);
        passScreen3.SetActive(false);
    }
}