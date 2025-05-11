const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

const BUILD_DIR = path.join(__dirname, 'Build');

// Get all files in the Build directory
const files = fs.readdirSync(BUILD_DIR);

// Upload each file to R2
for (const file of files) {
  const filePath = path.join(BUILD_DIR, file);
  if (fs.statSync(filePath).isFile()) {
    console.log(`Uploading ${file}...`);
    try {
      execSync(`npx wrangler r2 object put io-game-assets/Build/${file} --file=${filePath}`, { stdio: 'inherit' });
      console.log(`✅ Uploaded ${file}`);
    } catch (error) {
      console.error(`❌ Failed to upload ${file}:`, error.message);
    }
  }
} 