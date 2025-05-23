# IO Game Web Build

## Локальный запуск

### Вариант 1: Node.js с server.js (рекомендуемый)
```bash
# Перейдите в папку web-build
cd web-build

# Установите зависимости (только первый раз)
npm install

# Запустите сервер
node server.js
```

После запуска откройте в браузере:
```
http://localhost:8000
```

## Деплой на Cloudflare Pages с R2

1. Перейдите в папку web-build:
```bash
cd web-build
```

2. Установите зависимости:
```bash
npm install
```

3. Войдите в свой аккаунт Cloudflare (если еще не вошли):
```bash
npx wrangler login
```

4. Создайте R2 bucket для больших файлов:
```bash
npx wrangler r2 bucket create io-game-assets
```

5. Загрузите большие файлы в R2:
```bash
npm run upload-assets
```

6. Деплой проекта:
```bash
npm run deploy
```

После успешного деплоя вы получите URL вашего приложения.

## Примечания
- Порт 8000 можно заменить на любой другой свободный порт
- Для остановки сервера нажмите Ctrl+C в терминале
- Рекомендуется использовать современный браузер (Chrome, Firefox, Safari)
- Node.js вариант с server.js рекомендуется, так как он правильно обрабатывает сжатые файлы Unity WebGL 