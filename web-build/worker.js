export default {
  async fetch(request, env, ctx) {
    const url = new URL(request.url);
    let path = url.pathname;

    // Serve index.html for root path
    if (path === '/') {
      path = '/index.html';
    }

    // Get the file from the bucket
    const file = await env.ASSETS.fetch(request);
    
    // If file not found, return 404
    if (file.status === 404) {
      return new Response('Not Found', { status: 404 });
    }

    // Clone the response to modify headers
    const response = new Response(file.body, file);

    // Set proper headers for .br files
    if (path.endsWith('.br')) {
      response.headers.set('Content-Encoding', 'br');
      response.headers.set('Content-Type', 'application/octet-stream');
    }

    return response;
  }
}; 