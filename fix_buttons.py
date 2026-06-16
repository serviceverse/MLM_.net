import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # 1. Clean up buttons
    # Run multiple times to catch multiple instances if any
    for _ in range(3):
        content = re.sub(r'(<button[^>]*class="[^"]*)col-span-full mt-4\s*', r'\1', content)
        content = re.sub(r'(<button[^>]*class="[^"]*)flex-1\s*', r'\1', content)
        content = re.sub(r'(<button[^>]*class="[^"]*)max-w-xs\s*', r'\1', content)
        content = re.sub(r'(<button[^>]*class="[^"]*)max-w-sm\s*', r'\1', content)
    
    content = re.sub(r'(<button[^>]*class="[^"]*)(\s{2,})', r'\1 ', content)

    # 2. Add max-w-5xl to main page wrappers
    content = re.sub(
        r'(<div class="p-6 md:p-8">\s*)<div class="(w-full\s+)?space-y-[68]">',
        r'\1<div class="max-w-6xl space-y-6">',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Fixed buttons & layout: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
