export default {
    async fetch(request, env) {
      const url = new URL(request.url);
      let key = url.pathname.slice(1);      // прибираємо ведучий "/"
      if (!key || key.endsWith("/")) {
        key = "index.html";                 // за замовчуванням
      }
  
      const obj = await env.IO_GAME_ASSETS.get(key);
      if (!obj) {
        return new Response("Not found", { status: 404 });
      }
  
      const ext = key.split(".").pop().toLowerCase();
      const types = {
        html: "text/html",
        js:   "application/javascript",
        css:  "text/css",
        png:  "image/png",
        jpg:  "image/jpeg",
        json: "application/json",
        wasm: "application/wasm",
        unityweb: "application/octet-stream"
      };
      const contentType = types[ext] || "application/octet-stream";

      // Special handling for Unity WebGL files
      const headers = {
        "Content-Type": contentType,
        "Cache-Control": "max-age=3600",
      };

      // Add gzip encoding for Unity WebGL files
      if (key.endsWith('.unityweb')) {
        headers["Content-Encoding"] = "gzip";
      }
  
      return new Response(obj.body, {
        headers: headers
      });
    },
  };
  