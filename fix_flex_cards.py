import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # 1. Look for the problematic nested Card Wrapper
    # <div class="bg-[var(--bg-secondary)] border border-[var(--border-primary)] rounded-3xl">
    #     <div class="p-8">
    
    if re.search(r'<div class="bg-\[var\(--bg-secondary\)\] border border-\[var\(--border-primary\)\] rounded-3xl">\s*<div class="p-8">', content):
        content = re.sub(
            r'<div class="bg-\[var\(--bg-secondary\)\] border border-\[var\(--border-primary\)\] rounded-3xl">\s*<div class="p-8">',
            r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm flex flex-col flex-1 overflow-hidden">',
            content
        )
        # We removed a `<div class="p-8">` wrapper, so we need to remove one `</div>` at the end
        content = content.rstrip()
        if content.endswith('</div>'):
            content = content[:-6]
            
    # Also fix files that might have been partially processed but are missing the flex-1
    # Look for the "Main Card" div that DOES NOT have flex-1
    # Actually my previous script DID put `flex flex-col flex-1 overflow-hidden` in the Main Card.
    # But wait, did User/List.cshtml get processed by the FIRST update_tables.py script?
    # No, User/List.cshtml has `min-h-screen bg-[var(--bg-secondary)]... flex flex-col` which means it WAS processed by the SECOND script, but the SECOND script just replaced the outer wrapper and failed to replace the title block and the main card because it didn't match the regex!

    # Let's fix the Title Block in User/List.cshtml and similar files.
    # The title block looks like:
    # <!-- Header -->
    # <div class="flex items-center justify-between">
    #     <div>
    #         <h1 class="text-4xl...
    #         <p ...
    #     </div>
    #     <a ...>Add Client</a>
    # </div>
    # We want to change this into the standard <!-- Page Header --> format.
    # But since they might have an "Add Client" button, we need to preserve the button!
    
    def title_replace(match):
        title = match.group(1).strip()
        desc = match.group(2).strip()
        button = match.group(3).strip() if match.group(3) else ""
        return f'''<!-- Page Header -->
    <div class="mb-6 flex justify-between items-center">
        <div>
            <h1 class="text-2xl font-bold text-[var(--text-primary)]">{title}</h1>
            <p class="text-sm text-[var(--text-secondary)] mt-1">{desc}</p>
        </div>
        {button}
    </div>'''
    
    # Matches <div class="flex items-center justify-between"> \n <div> \n <h1>...</h1> \n <p>...</p> \n </div> \n <a...>...</a> \n </div>
    content = re.sub(
        r'<!-- Header -->\s*<div class="flex items-center justify-between">\s*<div>\s*<h1[^>]*>([^<]+)</h1>\s*<p[^>]*>([^<]+)</p>\s*</div>\s*(<a[^>]*>.*?</a>)?\s*</div>',
        title_replace,
        content,
        flags=re.DOTALL
    )

    # 3. Fix Search/Filters row padding
    content = re.sub(
        r'<!-- Filters -->\s*<div class="px-6 py-4 border-b border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\]">',
        r'<!-- Filters -->\n        <div class="px-6 py-4 border-b border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)] shrink-0">',
        content
    )
    
    # 4. Fix Pagination row to have shrink-0 so it doesn't get squished, and ensure it is correct
    content = re.sub(
        r'<!-- Pagination -->\s*<div class="px-6 py-4 border-t border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\]">',
        r'<!-- Pagination -->\n        <div class="px-6 py-4 border-t border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)] shrink-0 mt-auto">',
        content
    )

    # If it had the old pagination format:
    content = re.sub(
        r'<!-- Pagination -->\s*<div class="flex items-center justify-between mt-6">',
        r'<!-- Pagination -->\n        <div class="px-6 py-4 border-t border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)] shrink-0 mt-auto">',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
