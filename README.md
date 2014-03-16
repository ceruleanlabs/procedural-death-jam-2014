# Spacebar Steve and the Adventure of the Shiny Thing

We're doing this for the [Procedural Death Jam](http://proceduraldeathjam.com/).

## Unity

Download and install [Unity 4.3.4](https://unity3d.com/unity/download).

When you first load it up you might need to load up the test_scene from File > Load Scene.

## Deployment

When you want to deploy a new build, build a Web Player build called "rack" and
make sure it overrides the `rack` directory in the root. However don't override
`rack.html` because there are some custom things added to that we don't want to
override.

## Testing Locally

You can run `rackup` to run the web build locally.
