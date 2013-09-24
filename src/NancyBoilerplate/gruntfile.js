/**
 * Unfamiliar with Grunt? Here are some links that will allow you to learn more.
 *
 * - http://gruntjs.com
 * - http://gruntjs.com/getting-started
 * - http://howtonode.org/introduction-to-npm
 */

'use strict';

module.exports = function(grunt) {

    /**
     * Configuring the tasks available for the build process.
     */
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        /**
         * Analyses JavaScript files using JSHint for errors or potential problems.
         * You can customise the parameters by modifying the .jshintrc file.
         * - http://jshint.com/
         */
        jshint: {
            all: [
                'gruntfile.js',
                'assets/js/**/*.js',
                '!assets/js/vendor/**/*.js'
            ],
            options: {
                jshintrc: '.jshintrc'
            }
        },

        /**
         * Optimizes the JavaScript into a single file using the r.js optimizer.
         */
        requirejs: {
            compile: {
                options: {
                    name: 'main',
                    mainConfigFile: 'assets/js/config.js',
                    paths: {
                        requireJS: 'vendor/require'
                    },
                    include: 'requireJS',
                    preserveLicenseComments: false,
                    out: 'assets/js/boilerpate-<%= pkg.version %>.js'
                }
            }
        },

        /**
         * Compiles Sass into a CSS file.
         */
        sass: {
            dev: {
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            },

            /**
             * Creates a compressed version of the project style sheet.
             */
            dist: {
                options: {
                    style: 'compressed'
                },
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            }
        },

        /**
         * Updates the URLs to assets in the mark-up files.
         */
        'string-replace': {
            dist: {
                files: {
                    'Views/Shared/_Layout.cshtml': 'Views/Shared/_Layout.cshtml'
                },
                options: {
                    replacements: [

                        /**
                         * All the JS is now in a single file to reduce HTTP requests so
                         * the AMD loading via requireJS can be replaced by a traditional
                         * loading a single JS file.
                         */
                        {
                            pattern: '<script data-main="assets/js/config" src="assets/js/vendor/require.js"></script>',
                            replacement: '<script src="assets/js/boilerplate-<%= pkg.version %>.js"></script>'
                        }
                    ]
                }
            }
        }
    });

    /**
     * Loads libraries to assist with the build process.
     */
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-requirejs');
    grunt.loadNpmTasks('grunt-string-replace');

    /**
     * Organises the front end assets ready for a production environment.
     */
    grunt.registerTask('dist', ['jshint', 'sass:dist', 'requirejs', 'string-replace']);
};
