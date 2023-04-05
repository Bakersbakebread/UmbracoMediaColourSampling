# UmbracoMediaColourSampling

I was very inspired by Owain's tweet: https://twitter.com/OwainCodes/status/1642148092962123776.

Owain wanted to stylise his site colours based on media inside of Umbraco, I tried it out and got lost in fun. 

-- 

I wrote something that takes the focal point of the image from Umbraco and gets an average color, using ImageSharp.

![image](https://user-images.githubusercontent.com/29239704/230124890-d35b6577-9290-4c87-8986-82bef6486d9a.png)


## Have fun, too! 

Clone down the site and experiment moving around the focal point on an image. 

The magic happens in the `ColourService` and the `FocalPointRectangle` model. Try adjusting values in there and see what you can do! 

```
mkdir UmbracoMediaColourSampling
cd UmbracoMediaColourSampling
git clone https://github.com/Bakersbakebread/UmbracoMediaColourSampling.git .
dotnet run
```

You'll have to through the install window, but uSync does all the things for us afterwards.