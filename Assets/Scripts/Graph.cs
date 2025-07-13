using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName functionName;

    Transform[] points;

    private void Awake()
    {
        float step = 2f / resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        points = new Transform[resolution * resolution];
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution) {
                x = 0;
                z += 1;
            }

            Transform point = points[i] = Instantiate(pointPrefab);

            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;

            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }

    private void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;

            FunctionLibrary.Function f = FunctionLibrary.GetFunction(functionName);
            position.y = f(position.x, position.z, time);

            point.localPosition = position;
        }
    }
}
