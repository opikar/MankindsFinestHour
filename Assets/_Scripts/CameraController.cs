using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject arrow;

    bool blinking;
    [SerializeField]
    int i_index = 0;
    float f_speed;
    Vector3 direction;
    float f_range;

    float blinkTime;

    private Transform m_transform;


    // Use this for initialization
    void Start()
    {
        blinkTime = 2f;
        m_transform = transform;
        f_range = Mathf.Abs(transform.position.z - waypoints[i_index].position.z) + 0.1f;
        f_range *= f_range;
        GetDirection();

    }

    // Update is called once per frame
    void Update()
    {
        if (i_index == -1)
        {
            //Application.LoadLevel("BossLevel1");
            return;
        }

        m_transform.Translate(direction * f_speed * Time.deltaTime);

        if ((m_transform.position - waypoints[i_index].position).sqrMagnitude < f_range)
        {
            if (++i_index == waypoints.Length)
            {
                i_index = -1;
                return;
            }
            if (i_index != -1)
                GetDirection();
        }
        if (!blinking && i_index < waypoints.Length)
            if ((m_transform.position - waypoints[i_index].position).sqrMagnitude < 500f)
                StartCoroutine(BlinkingArrow());

    }
    private void GetDirection()
    {
        Vector3 vec;
        vec = m_transform.position;
        vec.z = 0;
        direction = (waypoints[i_index].position - vec).normalized;
        f_speed = waypoints[i_index].GetComponent<WaypointScript>().SpeedToWaypoint;
    }

    private IEnumerator BlinkingArrow()
    {
        if (i_index == waypoints.Length - 1) yield break;
        Vector3 p1 = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        float width = Vector3.Distance(p1, p2) * 0.4f;
        p2 = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        float height = Vector3.Distance(p1, p2) * .4f;
        blinking = true;
        int i = i_index;
        Vector3 dir = (waypoints[i_index + 1].position - waypoints[i_index].position).normalized;
        float angle = Vector3.Angle(transform.right, dir);
        if (Vector3.Dot(transform.up, dir) < 0)
            angle *= -1;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        float startTime = Time.time;
        while (startTime + blinkTime > Time.time && i == i_index)
        {
            Vector3 position = transform.position;
            position.z = 0;
            dir = dir.normalized;
            dir.x *= width;
            dir.y *= height;
            position += dir;
            arrow.transform.position = position;
            arrow.SetActive(!arrow.activeSelf);
            yield return new WaitForSeconds(0.4f);
        }
        blinking = false;
        arrow.SetActive(false);
    }


}
