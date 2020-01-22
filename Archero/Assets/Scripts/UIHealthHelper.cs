using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthHelper : MonoBehaviour
{
    private HealthHelper _target;

    public HealthHelper Target
    {
        get { return _target; }
        set { _target = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        if (GetComponent<Slider>().maxValue != Target.MaxHp)
            GetComponent<Slider>().maxValue = Target.MaxHp;
        if (GetComponent<Slider>().value != Target.Hp)
            GetComponent<Slider>().value = Target.Hp;

        if(gameObject.tag == "Enemy")
        {
                Vector3 newpos = new Vector3(Target.transform.position.x, Target.transform.position.y + Target.GetComponent<CapsuleCollider>().bounds.size.y, Target.transform.position.z);
                GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(newpos);
        }
        if(gameObject.tag == "Player")
        {
                Vector3 newpos = new Vector3(Target.transform.position.x, Target.transform.position.y + Target.GetComponent<CapsuleCollider>().bounds.size.y * 1.5f, Target.transform.position.z);
                GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(newpos);
        }
    }
}
