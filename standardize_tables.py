import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content
    
    # Check if this is one of our table files (it has the old full-screen wrapper)
    if 'class="flex flex-col h-full bg-[var(--bg-primary)] text-[var(--text-primary)]"' not in content:
        return False
        
    # 1. Update outer wrapper to the professional gray background with padding
    content = content.replace(
        'class="flex flex-col h-full bg-[var(--bg-primary)] text-[var(--text-primary)]"',
        'class="min-h-screen bg-[var(--bg-secondary)] text-[var(--text-primary)] p-6 md:p-8 flex flex-col"'
    )
    
    # Remove the inner flex-1 wrapper if it exists as it was before
    content = content.replace('<div class="flex-1 flex flex-col">', '')

    # 2. Extract Title and Description robustly
    title_match = re.search(r'<h1[^>]*>([^<]+)</h1>', content)
    desc_match = re.search(r'<p class="[^"]*text-\[var\(--text-secondary\)\][^"]*">([^<]+)</p>', content)
    
    title = title_match.group(1).strip() if title_match else "Page Title"
    desc = desc_match.group(1).strip() if desc_match else ""
    
    # Remove the old title block up to the flex-1 overflow-hidden
    # We will just replace everything from the start of the first inner div to the flex-1 overflow-hidden
    content = re.sub(
        r'<div class="flex items-center justify-between">.*?<div class="flex-1 overflow-hidden flex flex-col">',
        lambda m: f'''<!-- Page Header -->
    <div class="mb-6">
        <h1 class="text-2xl font-bold text-[var(--text-primary)]">{title}</h1>
        <p class="text-sm text-[var(--text-secondary)] mt-1">{desc}</p>
    </div>

    <!-- Main Card -->
    <div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm flex flex-col flex-1 overflow-hidden">''',
        content,
        flags=re.DOTALL
    )

    # If it was updated by the FIRST script, it might look like:
    content = re.sub(
        r'<!-- Header & Action Bar -->\s*<div class="px-6 py-4 bg-\[var\(--bg-secondary\)\].*?<!-- Action items will go here -->\s*</div>\s*<div class="flex-1 overflow-hidden flex flex-col">',
        lambda m: f'''<!-- Page Header -->
    <div class="mb-6">
        <h1 class="text-2xl font-bold text-[var(--text-primary)]">{title}</h1>
        <p class="text-sm text-[var(--text-secondary)] mt-1">{desc}</p>
    </div>

    <!-- Main Card -->
    <div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm flex flex-col flex-1 overflow-hidden">''',
        content,
        flags=re.DOTALL
    )

    # 3. Fix Search/Filters row padding
    content = re.sub(
        r'<!-- Filters -->\s*<div class="flex items-center justify-between mb-6">',
        r'<!-- Filters -->\n        <div class="px-6 py-4 border-b border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)]">',
        content
    )
    
    content = re.sub(
        r'<!-- Search Row -->\s*<div class="flex justify-between items-center mb-6">',
        r'<!-- Search Row -->\n        <div class="px-6 py-4 border-b border-[var(--border-secondary)] flex justify-between items-center bg-[var(--bg-primary)]">',
        content
    )
    
    content = re.sub(
        r'<!-- Search Row -->\s*<div class="flex justify-between items-center mb-4 px-6 py-4 bg-\[var\(--bg-primary\)\] border-b border-\[var\(--border-secondary\)\]">',
        r'<!-- Search Row -->\n        <div class="px-6 py-4 border-b border-[var(--border-secondary)] flex justify-between items-center bg-[var(--bg-primary)]">',
        content
    )

    # 4. Fix Pagination padding
    content = re.sub(
        r'<!-- Pagination -->\s*<div class="flex items-center justify-between mt-6">',
        r'<!-- Pagination -->\n        <div class="px-6 py-4 border-t border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)]">',
        content
    )

    # Ensure the end tags are balanced (we removed the inner flex-1 flex flex-col)
    # The original file ended with </div>\n</div>\n</div> (if flex-1 flex flex-col was present)
    # We replaced the outer with min-h-screen, removed the inner flex-1 flex flex-col (start tag),
    # so we have one extra closing </div> at the end. We need to remove the last </div>
    # Actually, we replaced the outer, replaced the flex-1 flex flex-col (start tag removed). 
    # Let's cleanly fix the end.
    if content != original_content:
        # Strip trailing whitespaces and fix ending divs
        content = content.rstrip()
        if content.endswith('</div>\n</div>\n</div>'):
             content = content[:-6] # Remove one </div>
             
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content + '\n')
        print(f"Updated: {filepath}")
        return True
    return False

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    updated = 0
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                if update_file(os.path.join(root, file)):
                    updated += 1
    print(f"Files standardized: {updated}")

if __name__ == "__main__":
    main()
