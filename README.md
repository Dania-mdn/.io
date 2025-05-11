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

## Деплой на Cloudflare Workers

1. Установите Wrangler CLI (если еще не установлен):
```bash
npm install -g wrangler
```

2. Войдите в свой аккаунт Cloudflare:
```bash
wrangler login
```

3. Деплой проекта:
```bash
cd web-build
wrangler deploy
```

После успешного деплоя вы получите URL вашего приложения.

## Примечания
- Порт 8000 можно заменить на любой другой свободный порт
- Для остановки сервера нажмите Ctrl+C в терминале
- Рекомендуется использовать современный браузер (Chrome, Firefox, Safari)
- Node.js вариант с server.js рекомендуется, так как он правильно обрабатывает сжатые файлы Unity WebGL 