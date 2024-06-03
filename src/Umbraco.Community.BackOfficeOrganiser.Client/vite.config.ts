import { defineConfig } from "vite";

export default defineConfig({
  build: {
    lib: {
      entry: "src/index.ts",
      formats: ["es"],
    },
    outDir: "../Umbraco.Community.BackOfficeOrganiser/wwwroot/App_Plugins/Umbraco.Community.BackOfficeOrganiser/dist/",
    sourcemap: true,
    rollupOptions: {
      external: [/^@umbraco/],
    },
  },
});