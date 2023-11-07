//===================================================
//  Copyright @ Markus Dullnig 2023
//  Author：Markus Dullnig
//  Time：2023-11-07 20:28:19
//  GitUser: azzinoth01
//===================================================
using System.Collections.Generic;

public interface INeuralNetworkBrain {

    public void Init();
    public void SetInputLayer(float[] input);
    public void SetInputLayer(List<float> input);
    public List<float> CalculateOutputData();
    public void Mutate();
    public void SetScore(float score);

}
