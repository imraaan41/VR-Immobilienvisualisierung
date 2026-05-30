# VR-Immobilienvisualisierung

```
git add .
git commit -m "Initial commit"
git remote add origin https://github.com/DEINNAME/DEIN-REPO.git
git branch -M main
git push -u origin main
```

## Wenn das GitHub-Repo schon Inhalte hat: erst pullen, dann pushen
```powershell
cd "C:\Pfad\zu\deinem\Projektordner"
git init
git lfs install
git lfs track "*.blend"
git remote add origin https://github.com/DEINNAME/DEIN-REPO.git
git branch -M main
git pull origin main --allow-unrelated-histories
git add .
git commit -m "Lokale Änderungen"
git push -u origin main
```

## Standard-Workflow: pullen, ändern, pushen
```powershell
cd "C:\Pfad\zu\deinem\Projektordner"
git pull
git add .
git commit -m "Änderungen"
git push
```

## Bei Konflikt: GitHub-Blender-Datei behalten
```powershell
git checkout --theirs DATEI.blend
git add DATEI.blend
git commit -m "GitHub-Blender-Datei behalten"
git push
```

## GitHub mit lokalem Stand überschreiben
```powershell
git push -u origin main --force
```
