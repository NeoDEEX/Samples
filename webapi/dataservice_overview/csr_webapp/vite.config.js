import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    },
  },
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7278', // HTTPS 백엔드 주소 예시
        changeOrigin: true,
        secure: false, // HTTPS 인증서 오류 무시 (로컬 개발 시 필요)
        //rewrite: (path) => path.replace(/^\/api/, ''),
      },
    },
  },
})
