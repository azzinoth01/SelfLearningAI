using System.Collections.Generic;

public interface INeuralNetworkBrain {

    public void Init();
    public void SetInputLayer(float[] input);
    public void SetInputLayer(List<float> input);
    public List<float> CalculateOutputData();
    public void Mutate();
    public void SetScore(float score);

}
