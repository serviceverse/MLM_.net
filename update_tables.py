import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content
    
    # 1. Update outer wrapper
    content = re.sub(
        r'class="(?:page-scroll-wrapper )?min-h-screen bg-primary text-\[var\(--text-primary\)\] p-8"',
        r'class="flex flex-col h-full bg-[var(--bg-primary)] text-[var(--text-primary)]"',
        content
    )

    # 2. Update inner wrapper
    content = re.sub(
        r'class="(?:page-scroll-content )?(?:px-8 py-6 |max-w-7xl mx-auto )?space-y-8"',
        r'class="flex-1 flex flex-col"',
        content
    )

    # 3. Update Title block (with h1 and p)
    # Match: <div>\s*<h1 class="text-4xl font-bold mb-2">(.+?)</h1>\s*<p class="text-\[var\(--text-secondary\)\]">(.+?)</p>\s*</div>
    def title_replace(match):
        title = match.group(1)
        desc = match.group(2)
        return f'''<!-- Header & Action Bar -->
        <div class="px-6 py-4 bg-[var(--bg-secondary)] border-b border-[var(--border-primary)] flex justify-between items-center shrink-0">
            <div>
                <h1 class="text-2xl font-bold">{title}</h1>
                <p class="text-sm text-[var(--text-secondary)] mt-1">{desc}</p>
            </div>
            <!-- Action items will go here -->
        </div>'''
    
    content = re.sub(
        r'<div>\s*<h1 class="text-4xl font-bold mb-2">(.+?)</h1>\s*<p class="text-\[var\(--text-secondary\)\]">(.+?)</p>\s*</div>',
        title_replace,
        content
    )

    # 4. Update the card wrapper holding the table
    content = re.sub(
        r'class="bg-\[var\(--bg-secondary\)\] border border-\[var\(--border-primary\)\] rounded-3xl p-8"',
        r'class="flex-1 overflow-hidden flex flex-col"',
        content
    )

    # 5. Update overflow-x-auto to flex-1 overflow-auto
    content = re.sub(
        r'class="overflow-x-auto"',
        r'class="flex-1 overflow-auto bg-[var(--bg-primary)]"',
        content
    )

    # 6. Update table headers to be sticky
    content = re.sub(
        r'<thead>\s*<tr class="border-b border-\[var\(--border-primary\)\]">',
        r'<thead class="bg-[var(--bg-tertiary)] sticky top-0 z-10 shadow-sm">\n                        <tr>',
        content
    )
    
    # Optional 6b: If it just has <thead>
    content = re.sub(
        r'<thead>\s*<tr>',
        r'<thead class="bg-[var(--bg-tertiary)] sticky top-0 z-10 shadow-sm">\n                        <tr>',
        content
    )
    
    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated: {filepath}")
        return True
    return False

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    updated_count = 0
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                if update_file(os.path.join(root, file)):
                    updated_count += 1
    print(f"Total files updated: {updated_count}")

if __name__ == "__main__":
    main()
