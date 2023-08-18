﻿using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : AInteractableObject
{
    private const string ANIM_BOOL_NAME = "isOpen_Obj_";

    [SerializeField] private Animator m_Animator;
    private bool m_IsOpen;

    [SerializeField] private int m_ObjectNumber;
    public int ObjectNumber => m_ObjectNumber;

    [SerializeField] private bool m_IsLocked;
    public bool Locked => m_IsLocked;

    [SerializeField] private string m_UnlockKey;
    [SerializeField] private List<CollectableItem> m_ItemsToUnlock;

    private void Start()
    {
        m_Animator.enabled = false;  // Disable animation states by default.  
    }

    public override void Interact()
    {
        if (Locked)
        {
            return;
        }

        string animBoolNameNum = ANIM_BOOL_NAME + ObjectNumber.ToString();
        bool isOpen = m_Animator.GetBool(animBoolNameNum); // Need current state for message.

        m_Animator.enabled = true;
        m_Animator.SetBool(animBoolNameNum, !isOpen);
    }

    public override void LookAt(GUIConfig guiController)
    {
        guiController.ShouldShowMsg = true;
        string animBoolNameNum = ANIM_BOOL_NAME + ObjectNumber.ToString();
        m_IsOpen = m_Animator.GetBool(animBoolNameNum); // Need current state for message.

        if (Locked)
        {
            guiController.ShowInteractMsg("It's locked. You need to find a way to unlock it first.");
        }
        else
        {
            guiController.ShowInteractMsg(m_IsOpen);
        }
    }

    public void Unlock()
    {
        m_IsLocked = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Locked && FactDB.GetBoolFact(m_UnlockKey))
            {
                Unlock();
                if (m_ItemsToUnlock.Count != 0)
                {
                    for (int i = 0; i < m_ItemsToUnlock.Count; i++)
                    {
                        CollectableItem itemToUnlock = m_ItemsToUnlock[i];
                        itemToUnlock.UseItem();
                    }
                }
            }
        }
    }

    protected override void Reset()
    {
        base.Reset();
        if (m_Animator == null)
        {
            m_Animator = GetComponentInParent<Animator>();
        }
    }
}