Shader "Unlit/StencilBlack"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        // All pixels in this SubShader pass the stencil test only if the current value of the stencil buffer is less than 2
        // You would typically do this if you wanted to only draw to areas of the render target that were not "masked out"
        Stencil
        {
            Ref 1
            Comp NotEqual
        }  

        // The rest of the code that defines the SubShader goes here.        

        Pass
        {    
              // The rest of the code that defines the Pass goes here.
        }
    }
}