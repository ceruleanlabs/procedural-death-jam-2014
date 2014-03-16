use Rack::Static,
  urls: ['/rack'],
  root: 'rack'

run lambda { |env|
  [
    200,
    {
      'Content-Type' => 'text/html',
      'Cache-Control' => 'public, max-age=86400'
    },
    File.open('rack/rack.html', File::RDONLY)
  ]
}
