export default {
  async fetch(request, env, ctx) {
    const url = new URL(request.url);
    const path = url.pathname;

    // Check if the request is for a large file
    if (path.startsWith('/Build/') && (
      path.endsWith('.data.br') || 
      path.endsWith('.wasm.br') || 
      path.endsWith('.framework.js.br')
    )) {
      // Get the file from R2
      const object = await env.ASSETS.get(path.slice(1));
      
      if (object === null) {
        return new Response('Not Found', { status: 404 });
      }

      // Create response with proper headers
      const headers = new Headers();
      headers.set('Content-Type', 'application/octet-stream');
      headers.set('Content-Encoding', 'br');
      headers.set('Cache-Control', 'public, max-age=31536000');

      return new Response(object.body, {
        headers,
      });
    }

    // For all other requests, serve from Pages
    return env.ASSETS.fetch(request);
  }
}; 