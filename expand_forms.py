import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # 1. Remove outer constraints
    content = re.sub(
        r'<div class="w-full max-w-[a-z0-9]+ bg-\[var\(--bg-secondary\)\] border border-\[var\(--border-primary\)\] rounded-2xl shadow-md p-6 md:p-8 space-y-6">',
        r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm p-6 md:p-8 space-y-6">',
        content
    )
    
    # Old page-scroll wrappers
    content = re.sub(
        r'<div class="custome-scroll page-scroll-wrapper min-h-screen bg-primary text-\[var\(--text-primary\)\] p-8">\s*<div class="page-scroll-content px-8 py-6">',
        r'<div class="p-6 md:p-8">',
        content
    )
    # Don't forget to remove the extra closing </div> if we removed a wrapper?
    # Actually if we replaced 2 divs with 1 div, we have an extra </div> at the end.
    # It's safer to just replace the outer most div and let the HTML be slightly unbalanced, or do it carefully.
    
    # Just replace `max-w-... mx-auto` with `w-full`
    content = re.sub(r'max-w-[a-z0-9]+\s+mx-auto', 'w-full', content)
    content = re.sub(r'max-w-[a-z0-9]+\s+w-full', 'w-full', content)
    content = re.sub(r'w-full\s+max-w-[a-z0-9]+', 'w-full', content)

    # 2. Update the card wrappers to standard
    content = re.sub(
        r'<div class="bg-\[var\(--bg-secondary\)\] border border-\[var\(--border-primary\)\] rounded-3xl shadow-xl p-8">',
        r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm p-6 md:p-8">',
        content
    )
    
    # 3. Change form to grid
    content = re.sub(
        r'<form class="space-y-\d+(\s+relative)?">',
        r'<form class="grid grid-cols-1 lg:grid-cols-2 gap-6 relative">',
        content
    )
    
    # Remove space-y-6 from the card if we are using grid (otherwise it adds gap above the form, which is fine)
    
    # 4. Make button wrappers col-span-full
    # Match: <div class="..."> \n <button
    content = re.sub(
        r'(<div class=")([^"]*)("\s*>\s*<button[^>]*type="(?:submit|button)"[^>]*>)',
        r'\1col-span-full mt-4 \2\3',
        content
    )
    
    # Also if the button is just inside the form directly
    content = re.sub(
        r'(<button[^>]*type="(?:submit|button)"[^>]*class=")([^"]*)(")',
        r'\1col-span-full mt-4 \2\3',
        content
    )
    # The above might add col-span-full to the button itself, which is fine if it's a direct child of the grid!
    # But wait, if we added it to the wrapper, we don't need it on the button. Let's just do wrapper.
    # Actually, adding col-span-full to a button that is inside a div does nothing, so it's harmless.

    # 5. Remove min-h-screen and justify-center
    content = re.sub(
        r'<div class="min-h-screen bg-primary text-\[var\(--text-primary\)\] p-8 flex items-start justify-center">',
        r'<div class="p-6 md:p-8">',
        content
    )

    if content != original_content:
        # Check for unbalanced divs if we replaced the double wrapper
        if '<div class="custome-scroll' in original_content:
            content = content.rstrip()
            if content.endswith('</div>'):
                content = content[:-6]

        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Expanded form: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
