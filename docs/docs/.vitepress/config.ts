import { defineConfig } from 'vitepress'

export default defineConfig({
    lang: 'en-US',
    title: 'DbgCensus Documentation',
    description: "Unofficial C# wrappers for Daybreak Game Company's Census API.",
    base: '/DbgCensus/',
    markdown: {
        theme: 'one-dark-pro',
        lineNumbers: true
    },
    themeConfig: {
        logo: '/icon.svg',
        nav: nav(),
        sidebar: sidebar(),
        socialLinks: [
            { icon: 'github', link: 'https://github.com/carlst99/DbgCensus' }
        ]
    }
});

function nav() {
    return [
        {
            text: 'Core Info',
            link: '/core-info',
            activeMatch: '/core-info'
        },
        {
            text: 'Making Queries',
            link: '/rest/',
            activeMatch: '/rest/'
        },
        // {
        //   text: 'Event Streaming',
        //   link: '/eventstream/index.md'
        // },
        {
            text: 'Changelog',
            link: 'https://github.com/carlst99/DbgCensus/blob/main/CHANGELOG.md'
        }
    ]
}

function sidebar() {
    return {
        '/rest/': [
            {
                text: 'Making Queries',
                items: [
                    { text: 'Setup', link: '/rest/' },
                    { text: 'Performing Queries', link: '/rest/basic-querying' },
                    { text: 'Advanced Configuration', link: '/rest/advanced' }
                ]
            }
        ]
    }
}
